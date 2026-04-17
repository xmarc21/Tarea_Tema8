using System;
using System.Globalization;

namespace Tarea_Tema8_DateTime
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo cultura = new CultureInfo("es-ES");

            Console.Write("Introduce tu nombre completo: ");
            string nombreCompleto = Console.ReadLine();

            int indiceEspacio = nombreCompleto.IndexOf(' ');
            string primerNombre;
            if (indiceEspacio > 0)
            {
                primerNombre = nombreCompleto.Substring(0, indiceEspacio);
            }
            else
            {
                primerNombre = nombreCompleto;
            }

            DateTime fechaNacimiento = DateTime.MinValue;
            bool fechaValida = false;

            while (!fechaValida)
            {
                Console.Write("Fecha de nacimiento (dd/MM/yyyy): ");
                string entradaFecha = Console.ReadLine();

                if (DateTime.TryParse(entradaFecha, cultura, DateTimeStyles.None, out fechaNacimiento))
                {
                    if (fechaNacimiento > DateTime.Today)
                    {
                        Console.WriteLine("No se permiten fechas de nacimiento en el futuro.");
                    }
                    else
                    {
                        fechaValida = true;
                    }
                }
                else
                {
                    Console.WriteLine("Formato inválido. Usa dd/MM/yyyy.");
                }
            }

            DateTime hoy = DateTime.Today;

            int edad = hoy.Year - fechaNacimiento.Year;
            if (hoy.Month < fechaNacimiento.Month || 
                (hoy.Month == fechaNacimiento.Month && hoy.Day < fechaNacimiento.Day))
            {
                edad--;
            }

            string fechaCumpleLarga = fechaNacimiento.ToString("dddd, dd 'de' MMMM 'de' yyyy", cultura);

            string signo = ObtenerSignoZodiaco(fechaNacimiento);

            DateTime proximoCumple;
            try
            {
                proximoCumple = new DateTime(hoy.Year, fechaNacimiento.Month, fechaNacimiento.Day);
            }
            catch
            {
                proximoCumple = fechaNacimiento.AddYears(hoy.Year - fechaNacimiento.Year);
            }

            if (proximoCumple < hoy)
            {
                proximoCumple = proximoCumple.AddYears(1);
            }

            int diasHastaCumple = (proximoCumple - hoy).Days;

            Console.WriteLine();
            Console.WriteLine($"Hola, {primerNombre}!");
            Console.WriteLine($"Tienes {edad} años.");
            Console.WriteLine($"Tu cumpleaños es el {fechaCumpleLarga}.");
            Console.WriteLine($"Tu signo del zodiaco es {signo}.");

            if (diasHastaCumple == 0)
            {
                Console.WriteLine("¡Felicidades! Hoy es tu cumpleaños 🎉");
            }
            else
            {
                Console.WriteLine($"Faltan {diasHastaCumple} días para tu próximo cumpleaños.");
            }

            Console.WriteLine("\nPulsa cualquier tecla para salir...");
            Console.ReadKey();
        }

        static string ObtenerSignoZodiaco(DateTime fecha)
        {
            int mes = fecha.Month;
            int dia = fecha.Day;

            if ((mes == 3 && dia >= 21) || (mes == 4 && dia <= 19)) return "Aries";
            if ((mes == 4 && dia >= 20) || (mes == 5 && dia <= 20)) return "Tauro";
            if ((mes == 5 && dia >= 21) || (mes == 6 && dia <= 20)) return "Géminis";
            if ((mes == 6 && dia >= 21) || (mes == 7 && dia <= 22)) return "Cáncer";
            if ((mes == 7 && dia >= 23) || (mes == 8 && dia <= 22)) return "Leo";
            if ((mes == 8 && dia >= 23) || (mes == 9 && dia <= 22)) return "Virgo";
            if ((mes == 9 && dia >= 23) || (mes == 10 && dia <= 22)) return "Libra";
            if ((mes == 10 && dia >= 23) || (mes == 11 && dia <= 21)) return "Escorpio";
            if ((mes == 11 && dia >= 22) || (mes == 12 && dia <= 21)) return "Sagitario";
            if ((mes == 12 && dia >= 22) || (mes == 1 && dia <= 19)) return "Capricornio";
            if ((mes == 1 && dia >= 20) || (mes == 2 && dia <= 18)) return "Acuario";
            if ((mes == 2 && dia >= 19) || (mes == 3 && dia <= 20)) return "Piscis";

            return "Desconocido";
        }
    }
}