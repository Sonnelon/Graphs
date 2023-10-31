using Graphs.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Graphs
{

    public partial class MainWindow : Window
    {
        List<List<Edge>> allPaths = new List<List<Edge>>(); 
        Graph graph = new  Graph();
        int targetWeight = 0;
        List<Edge> bestPath = new List<Edge>();
        Validation validation = new Validation();
        List<Edge> edges = new List<Edge>();

        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        public void FillListView()
        {
           
            ListBoxGraph.Items.Clear();

            foreach (var vertex in graph.adjacencyList)
            {
                foreach (var edgesAll in vertex.Value)
                {
                    string edgeString = $"{vertex.Key} -> {edgesAll.Destination} (ВЕС {edgesAll.Weight})";

                    ListBoxGraph.Items.Add(edgeString);
                }
            }
        }
      

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int weight = int.Parse(WeightGraph.Text.ToString());
                string source = StartGraph.Text.ToString();
                string destination = EndGraph.Text.ToString();

                Edge newEdge = new Edge();
                newEdge.Weight = weight;
                newEdge.Source = source;
                newEdge.Destination = destination;

                if (validation.EdgeName(newEdge.Source) && validation.EdgeName(newEdge.Destination) && validation.EdgeWeight(newEdge.Weight))
                {
                    graph.AddEdge(newEdge);
                    FillListView();
                }
                else
                {
                    MessageBox.Show("Ошибка ввода");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода");
            }

        }

       

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            graph.adjacencyList.Clear();

            allPaths.Clear();

            FillListView();
        }


        public void PrintBestPath()
        {
            ListBoxGraph.Items.Clear();

            FillListView();

            string lasr;

            lasr = bestPath[0].Destination;

            if (bestPath != null && bestPath.Any())
            {
                ListBoxGraph.Items.Add("Лучший путь:");

                for (int i = 0; i < bestPath.Count; i++)
                {
                    
                        ListBoxGraph.Items.Add($"{lasr} -> {bestPath[i].Source};");
                   
                       

                    lasr = bestPath[i].Source;
                }

                ListBoxGraph.Items.Add($"Общий вес: {bestPath.Sum(e => e.Weight)}");
            }
            else
            {
                ListBoxGraph.Items.Add("Лучший путь не найден.");
            }
        }



        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                targetWeight = int.Parse(TextBoxTotalWeight.Text);

                if (targetWeight >= 0 )
                {

                    allPaths = graph.FindAllPaths();

                    bestPath = graph.FindBestPath(allPaths, targetWeight);


                }

                PrintBestPath();

            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка ввода");
            }
          
        }
    }
}
