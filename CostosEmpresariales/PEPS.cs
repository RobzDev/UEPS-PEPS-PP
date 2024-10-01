using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CostosEmpresariales
{
    class PEPS
    {
        List<List<double>> lists = new List<List<double>>();
        double output;
        int result;
        public double saldo;
        public double existencia;
        public void ListModifier(List<double> list)
        {
            lists.Add(list);



        }
        public void ListModifier()
        {
            //lists.Add(list);

            Queue<List<double>> queue = new Queue<List<double>>(lists);
            while (queue.Count > 0)
            {
                List<double> first = queue.Peek();
                if (first[0] <= 0)
                {
                    this.lists.RemoveAt(0);
                    queue.Dequeue();
                }
                else
                {
                    break;
                }
            }



        }


        public double Debe(List<double> list)
        {
            ListModifier(list);
            double res = list[0] * list[1];
            return res;
        }
        public double Haber(double salida)
        {
            if (salida == 0)
            {
                return 0;
            }

            double haber = 0;
            double res = lists[0][0] - salida;
            if (res > 0)
            {
                lists[0][0] = res;
                haber = salida * lists[0][1];
            }
            else if (res <= 0)
            {
                double operation = lists[0][0] * lists[0][1];
                salida = Math.Abs(res);
                lists[0][0] = 0;
                ListModifier();
                haber = operation + Haber(salida);
            }

            return haber;
        }

    }

   
    class UEPS
    {
        List<List<double>> lists = new List<List<double>>(); 
        public double saldo;   
        public double existencia; 

        public void ListModifier(List<double> list)
        {
            lists.Add(list); 
        }

        
        public void ListModifier()
        {
            Stack<List<double>> stack = new Stack<List<double>>(lists); 

            while (stack.Count > 0)
            {
                List<double> last = stack.Peek(); 
                if (last[0] <= 0)
                {
                    this.lists.RemoveAt(lists.Count - 1); // Quitamos la última entrada de la lista
                    stack.Pop();
                }
                else
                {
                    break;
                }
            }
        }

        // Método para calcular el Debe
        public double Debe(List<double> list)
        {
            ListModifier(list); // Añade la entrada
            double res = list[0] * list[1]; // Multiplica cantidad * costo unitario
            return res;
        }

        // Método para calcular el Haber bajo la lógica UEPS
        public double HaberUEPS(double salida)
        {
            if (salida == 0)
            {
                return 0;
            }

            double haber = 0;
            int lastIndex = lists.Count - 1; // Comienza desde la última entrada (última en entrar)

            double res = lists[lastIndex][0] - salida; // Resta la salida de la última entrada

            if (res > 0)
            {
                lists[lastIndex][0] = res; // Actualiza la cantidad restante en la última entrada
                haber = salida * lists[lastIndex][1]; // Calcula el haber basado en el costo de la última entrada
            }
            else if (res <= 0)
            {
                double operation = lists[lastIndex][0] * lists[lastIndex][1]; // Todo lo que queda en esta última entrada
                salida = Math.Abs(res); // Calculamos cuánto falta después de agotar esta entrada
                lists[lastIndex][0] = 0; // Marcamos que esta entrada está agotada
                ListModifier(); // Limpiamos las entradas agotadas
                haber = operation + HaberUEPS(salida); // Continuamos el cálculo con la salida restante
            }

            return haber;
        }
    }

    public class PP
    {
        List<List<double>> lists = new List<List<double>>();
        public double saldo;          // Valor monetario total del inventario
        public double existencia;     // Cantidad de unidades en inventario
        public double costoPromedio;  // Costo promedio ponderado por unidad
        double costoMedio; // Costo promedio anterior

        // Método para calcular el Debe (registrar una entrada)
        public double Debe(List<double> list)
         {
            lists.Add(list);
            if (lists.Count > 1)
            {
                // lists[0][0] = lists[0][0] + lists[1][0];
                //lists[0][1] = lists[0][1] + lists[1][1];
                costoPromedio = (saldo + (lists[1][0] * lists[1][1])) / (lists[0][0] + lists[1][0]);
            }
            else
            {
                costoPromedio = list[1];
            }

            double entrada = list[0];       // Cantidad de unidades de la entrada
            double costoUnitario = list[1]; // Costo unitario de la entrada
           
            
            

        
          

            return entrada * costoUnitario;  // El debe es el costo total de la entrada
        }

        // Método para calcular el Haber (registrar una salida)
        public double Haber(double salida)
        {

            double res = lists[0][0] - salida;

            double haber = salida * costoPromedio;
           
            if (res > 0)
            {
                lists[0][0] = res;
               
            }
            else if (res <= 0)
            {
                
                double operation = (lists[0][0] + lists[1][0]) - salida;
                
                lists[0][0] = operation;
                lists[0][1] = costoPromedio;
                lists.RemoveAt(1);
               
            }


            return haber;  // El haber es el valor de la salida
        }
    }


}