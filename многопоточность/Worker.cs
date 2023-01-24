using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace multithreading
{
    public class Worker
    {
        private bool _cancelled = false;//флаг показывает отменен процесс или нет
        public void Cancel()
        {
            _cancelled = true;
        }
        public bool Work()
        {
            for (int i = 0; i <= 99; i++)
            {
                if (_cancelled)
                {
                    break;
                    
                }
                
                    Thread.Sleep(50);//останавливаем работу на 50 мс


                    ProcessChanged(i);//прдвинулись на итерацию, генерируем событие во вспомогательном потоке

            }
            return _cancelled;
        }
        
        public event Action<int> ProcessChanged;
    }
}
