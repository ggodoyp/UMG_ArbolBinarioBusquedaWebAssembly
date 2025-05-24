namespace ArbolBinarioBusquedaWebAssembly
{
    public class Nodo
    {
        public int Valor;
        public Nodo Izquierda;
        public Nodo Derecha;

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierda = null;
            Derecha = null;
        }
    }
}
