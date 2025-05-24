namespace ArbolBinarioBusquedaWebAssembly
{
    public class ArbolBinario
    {
        public Nodo raiz;
        int cantidad { get; set; }

        ////////////////////////////////////////////////////////////////                 INSERTAR DATOS                  //////////////////////////////////////////////////////////////
        public void PedirDatos()
        {
            Console.Write("¿Cuántos valores deseas insertar? ");
            cantidad = int.Parse(Console.ReadLine());
            for (int i = 1; i <= cantidad; i++)
            {
                Console.Write($"Valor #{i}: ");
                if (int.TryParse(Console.ReadLine(), out int valor))
                    Insertar(valor);
                else
                    Console.WriteLine("Valor inválido, se omite.");
            }
        }
        public void Insertar(int valor)
        {
            raiz = InsertarRecursivo(raiz, valor);
        }

        private Nodo InsertarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return new Nodo(valor);

            if (valor < nodo.Valor)
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, valor);
            else if (valor > nodo.Valor)
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, valor);
            else
                Console.WriteLine($"El valor {valor} ya está en el árbol.");

            return nodo;
        }

        /// ///////////////////////////////////////////////////////////////////////////        BUSCAR DATOS       /////////////////////////////////////////////////////////////////

        public void PreguntarBuscar()
        {
            Console.Write("¿Qué valor deseas buscar? ");
            if (int.TryParse(Console.ReadLine(), out int valorBuscado))
            {
                bool encontrado = Buscar(valorBuscado);
                Console.WriteLine(encontrado ? $"El valor {valorBuscado} fue encontrado." : $"El valor {valorBuscado} NO está en el árbol.");
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
            }
        }

        public bool Buscar(int valor)
        {
            return BuscarRecursivo(raiz, valor);
        }

        private bool BuscarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return false;

            if (valor == nodo.Valor)
                return true;
            else if (valor < nodo.Valor)
                return BuscarRecursivo(nodo.Izquierda, valor);
            else
                return BuscarRecursivo(nodo.Derecha, valor);
        }
        /// ///////////////////////////////////////////////////////////////////////////        ELIMINAR DATOS       /////////////////////////////////////////////////////////////////

        public void PreguntarEliminar()
        {
            Console.Write("¿Qué valor deseas eliminar? ");
            if (int.TryParse(Console.ReadLine(), out int valorEliminar))
            {
                Eliminar(valorEliminar);
                Console.WriteLine("EL VALOR SE A ELIMINADO CORRECTAMENTE");

            }
            else
            {
                Console.WriteLine("VALOR INVALIDO");
            }
        }
        public void Eliminar(int valor)
        {
            raiz = EliminarRecursivo(raiz, valor);
        }

        private Nodo EliminarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
            {
                Console.WriteLine($"EL VALOR {valor} NO SE ENCUENTRA EN EL ARBOL");
                return null;
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierda = EliminarRecursivo(nodo.Izquierda, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecha = EliminarRecursivo(nodo.Derecha, valor);
            }
            else
            {
                // sin hijos
                if (nodo.Izquierda == null && nodo.Derecha == null)
                {
                    return null;
                }

                // un solo hijo
                if (nodo.Izquierda == null)
                {
                    return nodo.Derecha;
                }
                else if (nodo.Derecha == null)
                {
                    return nodo.Izquierda;
                }

                // dos hijos
                Nodo sucesor = EncontrarMinimo(nodo.Derecha);
                nodo.Valor = sucesor.Valor;
                nodo.Derecha = EliminarRecursivo(nodo.Derecha, sucesor.Valor);
            }

            return nodo;
        }

        private Nodo EncontrarMinimo(Nodo nodo)
        {
            while (nodo.Izquierda != null)
            {
                nodo = nodo.Izquierda;
            }
            return nodo;
        }
        //////////////////////////////////////////////////////////////////////////////        RECORRIDO       /////////////////////////////////////////////////////////////////

        private void MostrarInOrdenRecursivo(Nodo nodo)
        {
            if (nodo == null) return;

            MostrarInOrdenRecursivo(nodo.Izquierda);
            Console.Write(nodo.Valor + " ->");
            MostrarInOrdenRecursivo(nodo.Derecha);
        }
        private void MostrarPostordenRecursivo(Nodo nodo)
        {
            if (nodo == null) return;

            MostrarPostordenRecursivo(nodo.Izquierda);
            MostrarPostordenRecursivo(nodo.Derecha);
            Console.Write(nodo.Valor + " ->");
        }
        private void MostrarPreordenRecursivo(Nodo nodo)
        {
            if (nodo == null) return;

            Console.Write(nodo.Valor + " ->");
            MostrarPreordenRecursivo(nodo.Izquierda);
            MostrarPreordenRecursivo(nodo.Derecha);
        }

        public void Recorrido()
        {
            Console.WriteLine();
            Console.WriteLine("RECORRIDO:");
            Console.WriteLine("In-Orden:");
            MostrarInOrdenRecursivo(raiz);
            Console.WriteLine();
            Console.WriteLine("Pre-Orden:");
            MostrarPreordenRecursivo(raiz);
            Console.WriteLine();
            Console.WriteLine("Post-Orden:");
            MostrarPostordenRecursivo(raiz);
            Console.WriteLine();
        }
        public string GetInOrden()
        {
            return InOrdenRec(raiz).TrimEnd('-', '>');
        }

        private string InOrdenRec(Nodo nodo)
        {
            if (nodo == null) return "";
            return InOrdenRec(nodo.Izquierda) + nodo.Valor + " ->" + InOrdenRec(nodo.Derecha);
        }
        public string GetPreOrden()
        {
            return PreOrdenRec(raiz).TrimEnd('-', '>');
        }

        private string PreOrdenRec(Nodo nodo)
        {
            if (nodo == null) return "";
            return nodo.Valor + " ->" + PreOrdenRec(nodo.Izquierda) + PreOrdenRec(nodo.Derecha);
        }
        public string GetPostOrden()
        {
            return PostOrdenRec(raiz).TrimEnd('-', '>');
        }

        private string PostOrdenRec(Nodo nodo)
        {
            if (nodo == null) return "";
            return PostOrdenRec(nodo.Izquierda) + PostOrdenRec(nodo.Derecha) + nodo.Valor + " ->";
        }

        public string ObtenerHtmlDelArbol()
        {
            if (raiz == null)
                return "<div>Árbol vacío</div>";

            return GenerarHtmlRecursivo(raiz);
        }

        private string GenerarHtmlRecursivo(Nodo nodo)
        {
            if (nodo == null)
                return "";

            string izquierda = GenerarHtmlRecursivo(nodo.Izquierda);
            string derecha = GenerarHtmlRecursivo(nodo.Derecha);

            return $@"
<div style='text-align: center; display: inline-block; margin: 10px;'>
    <div style='border: 1px solid black; border-radius: 50px; padding: 10px;'>{nodo.Valor}</div>
    <div style='display: flex; justify-content: center; gap: 20px;'>
        {(!string.IsNullOrEmpty(izquierda) ? izquierda : "<div style='width:50px;'></div>")}
        {(!string.IsNullOrEmpty(derecha) ? derecha : "<div style='width:50px;'></div>")}
    </div>
</div>";
        }



    }
}
