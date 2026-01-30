using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Estudos.Thread_ThreadPool_TPL_Cuda
{
    internal class T_TP_TPL_CUDA
    {
        static long RANGE_START = 200;
        static long RANGE_END = 800000;
        static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = PrimesInRange_01(RANGE_START, RANGE_END);
         
            Console.WriteLine($"PrimesInRange_01 - Foram encontrados {result} numeros primos em {sw.ElapsedMilliseconds / 1000} segundos com ({Environment.ProcessorCount} processadores).");

            sw.Restart();
            result = PrimesInRange_02(RANGE_START, RANGE_END);
            Console.WriteLine($"PrimesInRange_02 - Foram encontrados {result} numeros primos em {sw.ElapsedMilliseconds / 1000} segundos com ({Environment.ProcessorCount} processadores).");

            sw.Restart();
            result = PrimesInRange_03(RANGE_START, RANGE_END);
            Console.WriteLine($"PrimesInRange_03 - Foram encontrados {result} numeros primos em {sw.ElapsedMilliseconds / 1000} segundos com ({Environment.ProcessorCount} processadores).");

            sw.Restart();
            result = PrimesInRange_04(RANGE_START, RANGE_END);
            Console.WriteLine($"PrimesInRange_04 - Foram encontrados {result} numeros primos em {sw.ElapsedMilliseconds / 1000} segundos com ({Environment.ProcessorCount} processadores");

            sw.Restart();
            result = PrimesInRange_5(RANGE_START, RANGE_END);
            Console.WriteLine($"PrimesInRange_05 - Foram encontrados {result} numeros primos em {sw.ElapsedMilliseconds / 1000} segundos com ({Environment.ProcessorCount} processadores");

            sw.Stop();
        }


        /// <summary>
        /// primeira versao da utilização de um metodo para calcular numeros primos, isso executa em apenas 1 processador,
        /// nao ha paralelismo.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static long PrimesInRange_01(long start, long end)
        {
            long result = 0;
            for (var number = start; number < end; number++)
            {
                if (IsPrime(number))
                {
                    result++;
                }
            }
            return result;
        }


        /// <summary>
        /// nesse codigo ja começa a ter uso de threads para paralelismo, dividindo o trabalho entre os processadores
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static long PrimesInRange_02(long start, long end)
        {
            long result = 0;

            //esse lock é criado porque como estamos usando varias threads, o acesso a variavel result
            // pode ser corrompido, pois todas as threads tentam atualizar a mesma variavel, entao 
            // o lock garante que apenas uma thread atualize a variavel por vez.
            var lockObject = new object();
    
            var numberOfThreads = (long)Environment.ProcessorCount;
            var threads = new Thread[numberOfThreads];

            /*
                o start começa em 200, o end em 800000, então o range é a diferença entre os dois valores
                pois se fosse dividir o end pelo numero de processadores, a gente teria itens a mais desnecessarios,
            tendo em vista que nao vamos precisar calcular de 0 a 200
             */
            var range = end - start;

            /*
                Calcula o tamanho do pedaço do intervalo que cada thread irá processar.
                cada thread fica responsavel por um pedaço do intervalo total.
             */
            var tamanhoDoPedacoDeTrabalho = range / numberOfThreads;

            for (long threadAtual = 0; threadAtual < numberOfThreads; threadAtual++)
            {
                /*
                 * Ex: se o pedaço de trabalho for 10, o start for 10 e o end for 20, em 2 thread
                 * o tamanhoDoPedacoDeTrabalho vai ser 5 (range / threads)
                 * 
                 * threadAtual = 0
                 * entao pedacoInicial = 10 + 0 * 5 = 10
                 * pedacoFinal = (0 == 1) ? 20 : 10 + 5 = 15
                 * 
                 * Thread 0 processa os números: 10, 11, 12, 13, 14
                 * 
                 * Thread 1 (threadAtual = 1)
                 * pedacoInicial = 10 + 1 * 5 = 15;
                 * pedacoFinal = (1 == 1) ? 20 : 15 + 5 = 20;
                 * 
                 * Thread 1 processa os números: 15, 16, 17, 18, 19
                 */
                var pedacoInicial = start + threadAtual * tamanhoDoPedacoDeTrabalho;
                var pedacoFinal = (threadAtual == (numberOfThreads - 1)) ? end : pedacoInicial + tamanhoDoPedacoDeTrabalho;
                threads[threadAtual] = new Thread(() =>
                {
                    for (var number = pedacoInicial; number < pedacoFinal; ++number)
                    {
                        if (IsPrime(number))
                        {
                            lock (lockObject)
                            {
                                result++;
                            }
                        }
                    }
                });

                /*
                 antes dessa linha a gente apenas configurou cada thread, mas nao iniciou a execucao dela
                aqui a gente inicia a execucao da thread
                 */
                threads[threadAtual].Start();
            }

            //garantimos que cada thread finalize sua execucao antes de prosseguir
            foreach (var thread in threads)
            {
                //o metodo join aguarda a finalizacao da thread antes de prosseguir na execucao do codigo
                thread.Join();
            }

            return result;
        }


        /// <summary>
        /// basicamente o mesmo do PrimesInRange_02, porem utilizando o Interlocked.Increment para incrementar a variavel result
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static long PrimesInRange_03(long start, long end)
        {
            long result = 0;
            var range = end - start;
            var numberOfThreads = (long)Environment.ProcessorCount;

            var threads = new Thread[numberOfThreads];

            var chunkSize = range / numberOfThreads;

            for (long i = 0; i < numberOfThreads; i++)
            {
                var chunkStart = start + i * chunkSize;
                var chunkEnd = i == (numberOfThreads - 1) ? end : chunkStart + chunkSize;
                threads[i] = new Thread(() =>
                {
                    for (var number = chunkStart; number < chunkEnd; ++number)
                    {
                        if (IsPrime(number))
                        {
                            //basicamente, usar quando se quer incrementar uma variavel de forma atomica, ou seja, sem risco de corrupcao
                            //quando tiver um bloco de codigo, melhor usar lock
                            Interlocked.Increment(ref result);
                        }
                    }
                });

                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return result;
        }



        public static long PrimesInRange_04(long start, long end)
        {
            long result = 0;
            const long chunkSize = 100;

            var completed = 0;

            /*
                ManualResetEvent é usado para sinalizar quando todas as threads terminaram seu trabalho.
                A gente cria um ManualResetEvent com estado inicial "não sinalizado" (false).
                Uma ou mais threads podem chamar o método WaitOne() para aguardar até que o evento seja sinalizado.
                Outra thread pode chamar o método Set() para sinalizar o evento, liberando todas as threads que estão aguardando.
                O evento permanece sinalizado até que alguém chame Reset(), voltando ao estado não sinalizado.
             */
            var allDone = new ManualResetEvent(initialState: false);

            var chunks = (end - start) / chunkSize;

            for (long i = 0; i < chunks; i++)
            {
                var chunkStart = (start) + i * chunkSize;
                var chunkEnd = i == (chunks - 1) ? end : chunkStart + chunkSize;
                //thread pool reutiliza as threads existentes, ao inves de criar novas threads toda vez que precisa executar um trabalho
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    for (var number = chunkStart; number < chunkEnd; number++)
                    {
                        if (IsPrime(number))
                        {
                            Interlocked.Increment(ref result);
                        }
                    }

                    //quando o numero de chunks completados for igual ao numero total de chunks, sinaliza que todo o trabalho foi concluido
                    if (Interlocked.Increment(ref completed) == chunks)
                    {
                        allDone.Set();
                    }
                });

            }
            allDone.WaitOne(); //espera até que todas as tarefas terminem antes de retornar o resultado.
            return result;
        }

        /// <summary>
        /// executa de forma mais simples o paralelismo utilizando TPL,
        /// TPL significa Task Parallel Library, que é uma biblioteca do .NET para facilitar a criação e o gerenciamento de tarefas paralelas.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static long PrimesInRange_5(long start, long end)
        {
            long result = 0;
            Parallel.For(start, end, number =>
            {
                if (IsPrime(number))
                {
                    Interlocked.Increment(ref result);
                }
            });
            return result;
        }



        /// <summary>
        /// realiza o calculo de um numero primo
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static bool IsPrime(long number)
        {
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (long divisor = 3; divisor < (number / 2); divisor += 2)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
