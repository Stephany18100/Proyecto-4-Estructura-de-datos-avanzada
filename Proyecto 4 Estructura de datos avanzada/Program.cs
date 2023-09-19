using System;
using System.Collections.Generic;


public class Program
{
    public static void Main()
    {
        var agenda = new AgendaContactos();

        Console.WriteLine("Bienvenido a la Agenda de Contactos");

        while (true)
        {
            Console.WriteLine("\n¿Qué deseas hacer?");
            Console.WriteLine("1. Agregar un contacto");
            Console.WriteLine("2. Buscar un contacto por nombre");
            Console.WriteLine("3. Buscar contactos por inicial del nombre");
            Console.WriteLine("4. Eliminar un contacto");
            Console.WriteLine("5. Mostrar todos los contactos en orden alfabético");
            Console.WriteLine("6. Salir");

            int opcion;
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida. Por favor, ingresa un número válido.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese el nombre del contacto: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Ingrese la información de contacto y numero del contacto: ");
                    string informacionContacto = Console.ReadLine();
                    agenda.AgregarContacto(new Contacto(nombre, informacionContacto));
                    Console.WriteLine("Contacto agregado exitosamente.");
                    break;

                case 2:
                    Console.Write("Ingrese el nombre del contacto a buscar: ");
                    string nombreBusqueda = Console.ReadLine();
                    var contactoEncontrado = agenda.BuscarContactoPorNombre(nombreBusqueda);
                    if (contactoEncontrado != null)
                    {
                        Console.WriteLine($"Información de {contactoEncontrado.Nombre}: {contactoEncontrado.InformacionContacto}");
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró un contacto con el nombre '{nombreBusqueda}'.");
                    }
                    break;

                case 3:
                    Console.Write("Ingrese la inicial del nombre para buscar contactos: ");
                    char inicialBusqueda;
                    if (!char.TryParse(Console.ReadLine(), out inicialBusqueda))
                    {
                        Console.WriteLine("Inicial inválida. Por favor, ingresa una inicial válida.");
                        continue;
                    }
                    Console.WriteLine($"Contactos cuyos nombres empiezan con '{inicialBusqueda}':");
                    agenda.BuscarContactosPorInicial(inicialBusqueda);
                    break;

                case 4:
                    Console.Write("Ingrese el nombre del contacto a eliminar: ");
                    string nombreEliminar = Console.ReadLine();
                    agenda.EliminarContacto(nombreEliminar);
                    Console.WriteLine("Contacto eliminado exitosamente.");
                    break;

                case 5:
                    Console.WriteLine("Todos los contactos en orden alfabético:");
                    agenda.MostrarContactosEnOrden();
                    break;

                case 6:
                    Console.WriteLine("Gracias por utilizar la Agenda de Contactos. ¡Hasta luego!");
                    return;

                default:
                    Console.WriteLine("Opción inválida. Por favor, selecciona una opción válida.");
                    break;
            }
        }
    }
    public class Contacto
    {
        public string Nombre { get; set; }
        public string InformacionContacto { get; set; }

        public Contacto(string nombre, string informacionContacto)
        {
            Nombre = nombre;
            InformacionContacto = informacionContacto;
        }
    }
    public class NodoBST
{
    public Contacto Contacto { get; set; }
    public NodoBST Izquierdo { get; set; }
    public NodoBST Derecho { get; set; }

    public NodoBST(Contacto contacto)
    {
        Contacto = contacto;
        Izquierdo = null;
        Derecho = null;
    }
}

public class AgendaContactos
{
    private NodoBST raiz;

    public AgendaContactos()
    {
        raiz = null;
    }

    public void AgregarContacto(Contacto nuevoContacto)
    {
        raiz = AgregarContactoRecursivo(raiz, nuevoContacto);
    }

    private NodoBST AgregarContactoRecursivo(NodoBST nodo, Contacto nuevoContacto)
    {
        if (nodo == null)
        {
            return new NodoBST(nuevoContacto);
        }

        if (string.Compare(nuevoContacto.Nombre, nodo.Contacto.Nombre) < 0)
        {
            nodo.Izquierdo = AgregarContactoRecursivo(nodo.Izquierdo, nuevoContacto);
        }
        else if (string.Compare(nuevoContacto.Nombre, nodo.Contacto.Nombre) > 0)
        {
            nodo.Derecho = AgregarContactoRecursivo(nodo.Derecho, nuevoContacto);
        }

        return nodo;
    }

    public Contacto BuscarContactoPorNombre(string nombre)
    {
        return BuscarContactoPorNombreRecursivo(raiz, nombre);
    }

    private Contacto BuscarContactoPorNombreRecursivo(NodoBST nodo, string nombre)
    {
        if (nodo == null)
        {
            return null;
        }

        if (string.Compare(nombre, nodo.Contacto.Nombre) == 0)
        {
            return nodo.Contacto;
        }

        if (string.Compare(nombre, nodo.Contacto.Nombre) < 0)
        {
            return BuscarContactoPorNombreRecursivo(nodo.Izquierdo, nombre);
        }
        else
        {
            return BuscarContactoPorNombreRecursivo(nodo.Derecho, nombre);
        }
    }

    public void BuscarContactosPorInicial(char inicial)
    {
        BuscarContactosPorInicialRecursivo(raiz, inicial);
    }

    private void BuscarContactosPorInicialRecursivo(NodoBST nodo, char inicial)
    {
        if (nodo == null)
        {
            return;
        }

        if (nodo.Contacto.Nombre[0] == inicial)
        {
            Console.WriteLine($"{nodo.Contacto.Nombre}: {nodo.Contacto.InformacionContacto}");
        }

        BuscarContactosPorInicialRecursivo(nodo.Izquierdo, inicial);
        BuscarContactosPorInicialRecursivo(nodo.Derecho, inicial);
    }

    public void EliminarContacto(string nombre)
    {
        raiz = EliminarContactoRecursivo(raiz, nombre);
    }

    private NodoBST EliminarContactoRecursivo(NodoBST nodo, string nombre)
    {
        if (nodo == null)
        {
            return null;
        }

        if (string.Compare(nombre, nodo.Contacto.Nombre) < 0)
        {
            nodo.Izquierdo = EliminarContactoRecursivo(nodo.Izquierdo, nombre);
        }
        else if (string.Compare(nombre, nodo.Contacto.Nombre) > 0)
        {
            nodo.Derecho = EliminarContactoRecursivo(nodo.Derecho, nombre);
        }
        else
        {
            if (nodo.Izquierdo == null)
            {
                return nodo.Derecho;
            }
            else if (nodo.Derecho == null)
            {
                return nodo.Izquierdo;
            }

            NodoBST sucesor = EncontrarMinimo(nodo.Derecho);
            nodo.Contacto = sucesor.Contacto;
            nodo.Derecho = EliminarContactoRecursivo(nodo.Derecho, sucesor.Contacto.Nombre);
        }

        return nodo;
    }

    private NodoBST EncontrarMinimo(NodoBST nodo)
    {
        while (nodo.Izquierdo != null)
        {
            nodo = nodo.Izquierdo;
        }
        return nodo;
    }

    public void MostrarContactosEnOrden()
    {
        MostrarContactosEnOrdenRecursivo(raiz);
    }

    private void MostrarContactosEnOrdenRecursivo(NodoBST nodo)
    {
        if (nodo != null)
        {
            MostrarContactosEnOrdenRecursivo(nodo.Izquierdo);
            Console.WriteLine($"{nodo.Contacto.Nombre}: {nodo.Contacto.InformacionContacto}");
            MostrarContactosEnOrdenRecursivo(nodo.Derecho);
        }
    }
}
}


