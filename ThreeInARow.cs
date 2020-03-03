using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRaya
{
    class ThreeInARow
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nBienvenido al Tres en Raya"); //Saludo
            char[,] tablero = new char[,] { {' ',' ' ,' '  }, { ' ', ' ', ' ' }, { ' ', ' ', ' '} }; //Declaramos nuestra matriz (tablero)
            char respuesta; //Respuesta para el do while del programa principal

            //El programa dentro de un do while para poder repetirlo con una pregunta al final
            do
            {
                char jugador1;
                char jugador2 = ' ';

                Console.WriteLine("\n¿Listos para jugar?:");
                Console.WriteLine("\nJugador 1 que quieres usar 'x' o 'o': ");

                jugador1 = EleccionSimbolo();
                if (jugador1 == 'x') jugador2 = 'o';
                if (jugador1 == 'o') jugador2 = 'x';
                Console.WriteLine("\nJugador 1 eres: {0}, Jugador 2 eres: {1}", jugador1, jugador2);

                //Hice un apaño de booleanas para que el mensaje de has ganado jugador x no se repities, no se si estara muy bien ya que he ido parcheandolo mientras lo provaba.
                bool victoria1 = false;
                bool victoria2 = false;

                //Do while para los turnos mientras que lleno y victoria devuelvan false
                do
                {
                    //El número final es para personalizar el mensaje
                    MostrarTablero(tablero);
                    EditarTablero(tablero, jugador1, 1);
                    victoria1 = Victoria(tablero, jugador1, jugador2);
                    if (victoria1) MostrarTablero(tablero);
                    if (!victoria1)
                    {
                        MostrarTablero(tablero);
                        EditarTablero(tablero, jugador2, 2);
                    }
                    if(!victoria1) victoria2 = Victoria(tablero, jugador1, jugador2);
                    if (victoria2) MostrarTablero(tablero);

                } while (!Lleno(tablero) && !victoria1 && !victoria2);

                if (Lleno(tablero) && !Victoria(tablero, jugador1, jugador2)) Console.WriteLine("\nEl tablero esta lleno");
                //Pedimos si quiere volver a jugar o cerrar comprobando si la respuesta es un char para que no de error y si es 'n' o 'c'
                
                do
                {
                    Console.WriteLine("\nPara jugar de nuevo introduce 'n', para cerrar introduce 'c': ");
                    var respuestaintroducida = Console.ReadLine();

                    while (!char.TryParse(respuestaintroducida, out respuesta))
                    {
                        Console.WriteLine("Lo que has introducido no es una respuesta valida, intentalo de nuevo 'n' juego nuevo y 'c' para cerrar:");
                        respuestaintroducida = Console.ReadLine();
                    }
                    respuesta = char.Parse(respuestaintroducida);

                } while (respuesta != 'n' && respuesta != 'c');

                //Para vaciar limpiar el tablero
                if (respuesta == 'n')
                {
                    Console.WriteLine("\nJuego reiniciado");
                    tablero = new char[,] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
                }

            } while (respuesta == 'n');//Y repetir el juego

            //Mensaje de despedida y cerrar el programa
            if (respuesta == 'c')
            {
                Console.WriteLine("\nGracias por jugar a Tres en Raya, pulsa cualquier tecla para cerrar...");
                Console.ReadKey();
            }
        }

        //Función para la eleccion de 'x' o 'o'
        public static char EleccionSimbolo()
        {
            char j1;
            var eleccion = Console.ReadLine();
            do
            {
                while (!char.TryParse(eleccion, out j1))
                {
                    Console.WriteLine("Lo que has introducido no es una respuesta valida, elige 'x' o 'o':");
                    eleccion = Console.ReadLine();
                }
                j1 = char.Parse(eleccion);
            } while(j1 != 'x' && j1 != 'o');

            return j1;
        }

        //Función para mostrar la matriz(tablero)
        public static void MostrarTablero(char[,] tablero)
        {
            Console.WriteLine("\nTablero de juego");
            Console.WriteLine("\nPosición '1' '2' '3'\n      '1' {0}   {1}   {2} \n      '2' {3}   {4}   {5} \n      '3' {6}   {7}   {8}"
                , tablero[0, 0], tablero[0, 1], tablero[0, 2], tablero[1, 0], tablero[1, 1], tablero[1, 2], tablero[2, 0], tablero[2, 1], tablero[2, 2]);
        }

        //Función para poner las fichas
        private static char[,] EditarTablero(char[,] tablero, char fichaj, int numeroj)
        {
            char[,] mitablero = tablero;
            if (!Lleno(tablero))
            {
                int fila = 0;
                int columna = 0;

                Console.WriteLine("\nJugador {0} introduce tu posicion primero la fila y luego la columna: ", numeroj);
                do
                {
                    //Pedimos la fila
                    do
                    {
                        Console.WriteLine("Fila: ");
                        var introducido = Console.ReadLine();
                        while (!int.TryParse(introducido, out fila))
                        {
                            Console.WriteLine("Fila: "); ;
                            introducido = Console.ReadLine();
                        }
                        fila = int.Parse(introducido) - 1;

                    } while (fila != 0 && fila != 1 && fila != 2);
                    //Pedimos la columna
                    do
                    {
                        Console.WriteLine("Columna: ");
                        var introducido = Console.ReadLine();
                        while (!int.TryParse(introducido, out columna))
                        {
                            Console.WriteLine("Columna: "); ;
                            introducido = Console.ReadLine();
                        }
                        columna = int.Parse(introducido) - 1;

                    } while (columna != 0 && columna != 1 && columna != 2);
                    if (!Posicion(mitablero[fila, columna])) Console.WriteLine("\nEsta casilla esta ocupada intentalo de nuevo.");
                } while (!Posicion(mitablero[fila, columna]));//Usamos la función de comprobación para avisar y negar.
                mitablero[fila, columna] = fichaj;
            }
            return tablero;
        }

        //Función de comprobación, si esta vacío da true, si no false
        private static bool Posicion(char comprobacion) 
        {
            if (comprobacion == ' ') return true;
            else return false;  
        }
        
        //Comprobamos el tablero esta lleno
        private static bool Lleno(char[,]tablero)
        {
            char[,] mitablero = tablero;

            //Comprobamos si el tablero esta lleno
            int casillasOcupadas = 0;
            for (int f = 0; f < 3; f++) //for para la fila
            {
                for (int c = 0; c < 3; c++)//for para la columna
                { 
                    if (mitablero[f, c] != ' ')
                    {
                        casillasOcupadas++;
                    }
                }
            }

            //Booleanos si esta lleno true si no false
            if (casillasOcupadas == tablero.Length)
            {
                return true;
            }
            else return false;
        }

        //Comprobamos si alguien a ganado
        private static bool Victoria(char[,]tablero, char j1, char j2)
        {
            int filax = 0;
            int filao = 0;

            //Comprobamos cada fila
            for (int f = 0; f < 3; f++)
            {
                filax = 0; //Lo inicializamos a cero para que en cada linea vuelva a contar de nuevo
                filao = 0;
                for (int c = 0; c < 3; c++)
                {
                    if (tablero[f,c] == 'x') filax++;
                    if (tablero[f, c] == 'o') filao++;
                    if (filax == 3)//Para identificar al jugador recojo en la función el caracter que se asignaron y lo uso para dar un mensaje personalizado 
                    {
                        if (j1 == 'x') Console.WriteLine("\nJugador 1 has ganado");
                        if (j2 == 'x') Console.WriteLine("\nJugador 2 has ganado");
                        return true;
                    }
                    if (filao == 3)
                    {
                        if (j1 == 'o') Console.WriteLine("\nJugador 1 has ganado");
                        if (j2 == 'o') Console.WriteLine("\nJugador 2 has ganado");
                        return true;
                    }
                }
            }

            //Comprobamos cada columna (Se que la letra no hace falta cambiarla pero es para que sea más visual)
            for (int c = 0; c < 3; c++)
            {
                filax = 0;
                filao = 0;
                for (int f = 0; f < 3; f++)
                {
                    if (tablero[f, c] == 'x') filax++;
                    if (tablero[f, c] == 'o') filao++;
                    if (filax == 3)
                    {
                        if (j1 == 'x') Console.WriteLine("\nJugador 1 has ganado");
                        if (j2 == 'x') Console.WriteLine("\nJugador 2 has ganado");
                        return true;
                    }
                    if (filao == 3)
                    {
                        if (j1 == 'o') Console.WriteLine("\nJugador 1 has ganado");
                        if (j2 == 'o') Console.WriteLine("\nJugador 2 has ganado");
                        return true;
                    }
                }
            }

            //Comprobamos las diagonales
            //Diagonal de arriba a la izquierda a abajo a la derecha
            if (tablero[0, 0] == 'x' && tablero[1, 1] == 'x' && tablero[2, 2] == 'x')
            {
                if (j1 == 'x') Console.WriteLine("\nJugador 1 has ganado");
                if (j2 == 'x') Console.WriteLine("\nJugador 2 has ganado");
                return true;
            }
            if (tablero[0, 0] == 'o' && tablero[1, 1] == 'o' && tablero[2, 2] == 'o')
            {
                if (j1 == 'o') Console.WriteLine("\nJugador 1 has ganado");
                if (j2 == 'o') Console.WriteLine("\nJugador 2 has ganado");
                return true;
            }

            //Diagonal de abajo a la izquierda a arriba a la derecha
            if (tablero[2, 0] == 'x' && tablero[1, 1] == 'x' && tablero[0, 2] == 'x')
            {
                if (j1 == 'x') Console.WriteLine("\nJugador 1 has ganado");
                if (j2 == 'x') Console.WriteLine("\nJugador 2 has ganado");
                return true;
            }
            if (tablero[2, 0] == 'o' && tablero[1, 1] == 'o' && tablero[0, 2] == 'o')
            {
                if (j1 == 'o') Console.WriteLine("\nJugador 1 has ganado");
                if (j2 == 'o') Console.WriteLine("\nJugador 2 has ganado");
                return true;
            }

            else return false;
        }
    }
}
