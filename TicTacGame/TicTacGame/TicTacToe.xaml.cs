using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace TicTacGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicTacToe : ContentPage
    {
        Label[,] tic = new Label[3, 3];
        string l;
        public TicTacToe()
        {
            Reset();
            stps = 0;
            man.IsEnabled = true;
        }
        //InitializeComponent();
        Label stat;
        Button change, res, man;

        void Reset()
        {
            stps = 0;
            Grid grid = new Grid();
            for (int g = 0; g < 3; g++)
            {

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            for (int f = 0; f < 3; f++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            change = new Button
            {
                Text = "Random player"
            };
            change.Clicked += Change_Clicked;
            res = new Button
            {
                Text = "Reset game"
            };
            res.Clicked += Res_Clicked;
            man = new Button
            {
                Text = "Manual player selection"
            };
            man.Clicked += Man_Clicked;

            for (int i = 0; i < 3; i++) //Генерация поля
            {
                for (int j = 0; j < 3; j++)
                {



                    stat = new Label
                    {
                        BackgroundColor = Color.Green,
                        FontSize = 30,
                        Text = "",
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Black,
                        VerticalTextAlignment = TextAlignment.Center,


                    };
                    tic[i, j] = stat;
                    l = "X";
                    var tap = new TapGestureRecognizer();
                    tap.Tapped += Tap_Tapped;
                    grid.Children.Add(stat, i, j);
                    stat.GestureRecognizers.Add(tap);


                }

            }

            grid.Children.Add(change, 0, 3);
            grid.Children.Add(res, 2, 3);
            grid.Children.Add(man, 1, 3);
            Content = grid;

        }
        private async void Man_Clicked(object sender, EventArgs e)
        {
            string choice = await DisplayActionSheet("Кто начинает?", "X", "0", "Выбирайте знак");

            if (choice == "X")
            {
                chck = 2;
                change.Text = "X";
                man.IsEnabled = false;

            }
            else if (choice == "0")
            {

                chck = 1;
                change.Text = "0";
                man.IsEnabled = false;

            }

            else
            {
                man.IsEnabled = true;
            }
        }


        private void Res_Clicked(object sender, EventArgs e)
        {
            Reset();
            chck = 0;//Перезапуск игры
            stps = 0;
        }


        private void Tap_Tapped(object sender, EventArgs e)

        {


            Label stat = sender as Label;



            if (stat.Text == "") //Проверка на наличие текста в поле

                if (chck % 2 == 0)
                {
                    change.Text = "0";
                    stat.Text = l;
                    chck++;
                    stps++;



                }
                else if (chck % 2 != 0)
                {
                    change.Text = "X";
                    chck++;
                    stps++;
                    stat.Text = "0";

                }

            if (checkDraw() == true)
            {
                DisplayAlert("Игра окончена", "Ничья", "Новая игра");
                Reset();
                stps = 0;
            }

            else if (checkWinnerY() == true)
            {
                DisplayAlert("Игра окончена", wnr, "Новая игра");
                Reset();
            }
            else if (checkWinnerX() == true)
            {
                DisplayAlert("Игра окончена", wnr, "Новая игра");
                Reset();
            }
            else if (checkWinnerXY() == true)
            {
                DisplayAlert("Игра окончена", wnr, "Новая игра");
                Reset();
            }


        }
        bool checkDraw() //Проверка на ничью
        {
            if (stps == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        string wnr = "";
        bool checkWinnerX() //Проверка на победителя по диагонали
        { //по диагонале
            if (tic[0, 0].Text == "X" && tic[1, 0].Text == "X" && tic[2, 0].Text == "X")
            {
                wnr = "Выиграл X";
                return true; ;
            }
            else if (tic[0, 1].Text == "X" && tic[1, 1].Text == "X" && tic[2, 1].Text == "X")
            {
                wnr = "Выиграл X";
                return true;
            }
            else if (tic[0, 2].Text == "X" && tic[1, 2].Text == "X" && tic[2, 2].Text == "X")
            {
                wnr = "Выиграл X";
                return true;
            }
            else if (tic[0, 0].Text == "0" && tic[1, 0].Text == "0" && tic[2, 0].Text == "0")
            {
                wnr = "Выиграл 0";
                return true; ;
            }
            else if (tic[0, 1].Text == "0" && tic[1, 1].Text == "0" && tic[2, 1].Text == "0")
            {
                wnr = "Выиграл 0";
                return true;
            }
            else if (tic[0, 2].Text == "0" && tic[1, 2].Text == "0" && tic[2, 2].Text == "0")
            {
                wnr = "Выиграл 0";
                return true;
            }
            else
            {
                return false;
            }
        }
        bool checkWinnerY() //Проверка на победителя по вертикали
        {  //по горизонтали 

            if (tic[0, 0].Text == "X" && tic[0, 1].Text == "X" && tic[0, 2].Text == "X")
            {
                wnr = "Выиграл X";
                return true; ;
            }

            else if (tic[1, 0].Text == "X" && tic[1, 1].Text == "X" && tic[1, 2].Text == "X")
            {
                wnr = "Выиграл X";
                return true;
            }

            else if (tic[2, 0].Text == "X" && tic[2, 1].Text == "X" && tic[2, 2].Text == "X")
            {
                wnr = "Выиграл X";
                return true;
            }

            else if (tic[0, 0].Text == "0" && tic[0, 1].Text == "0" && tic[0, 2].Text == "0")
            {
                wnr = "Выиграл 0";
                return true; ;
            }

            else if (tic[1, 0].Text == "0" && tic[1, 1].Text == "0" && tic[1, 2].Text == "0")
            {
                wnr = "Выиграл 0";
                return true;
            }

            else if (tic[2, 0].Text == "0" && tic[2, 1].Text == "X" && tic[2, 2].Text == "0")
            {
                wnr = "Выиграл 0";
                return true;
            }
            else
            {
                return false;
            }
        }
        bool checkWinnerXY()
        {
            if (tic[0, 0].Text == "X" && tic[1, 1].Text == "X" && tic[2, 2].Text == "X")
            {
                wnr = "Выиграл X";
                return true; ;
            }
            else if (tic[2, 0].Text == "X" && tic[1, 1].Text == "X" && tic[0, 2].Text == "X")
            {
                wnr = "Выиграл X";
                return true;
            }
            else if (tic[0, 0].Text == "0" && tic[1, 1].Text == "0" && tic[2, 2].Text == "0")
            {
                wnr = "Выиграл 0";
                return true; ;
            }
            else if (tic[2, 0].Text == "0" && tic[1, 1].Text == "0" && tic[0, 2].Text == "0")
            {
                wnr = "Выиграл 0";
                return true;
            }
            else
            {
                return false;
            }
        }
        int stps = 0;


        Random strt = new Random();
        int chck = 0;
        private void Change_Clicked(object sender, EventArgs e) //Рандомайзер, кто будет ходить первый
        {
            chck = strt.Next(0, 2);
            if (chck % 2 == 0)
            {
                change.Text = "X";
            }
            else if (chck % 2 != 0)
            {
                change.Text = "0";
            }

        }


    }
}

