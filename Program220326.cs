using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
   
    static List<string> nomes = new List<string>();
    static List<string> gruposMusculares = new List<string>();
    static List<double> cargas = new List<double>();
    static List<int> repeticoes = new List<int>();

    static void Main(string[] args)
    {
        int opcao;

        do
        {
            ExibirMenu();
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida! Digite um número.");
                continue;
            }

            switch (opcao)
            {
                case 1: AdicionarExercicio(); break;
                case 2: ListarExercicios(); break;
                case 3: BuscarPorNome(); break;
                case 4: FiltrarPorGrupo(); break;
                case 5: CalcularCargaTotal(); break;
                case 6: ExibirMaisPesado(); break;
                case 7: RemoverExercicio(); break;
                case 0: Console.WriteLine("Saindo do sistema..."); break;
                default: Console.WriteLine("Opção inexistente!"); break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static void ExibirMenu()
    {
        Console.Clear();
        Console.WriteLine("=== SISTEMA DE TREINO ACADEMIA ===");
        Console.WriteLine("1 - Adicionar exercício");
        Console.WriteLine("2 - Listar exercícios");
        Console.WriteLine("3 - Buscar exercício por nome");
        Console.WriteLine("4 - Filtrar por grupo muscular");
        Console.WriteLine("5 - Calcular carga total do treino");
        Console.WriteLine("6 - Exibir exercício mais pesado");
        Console.WriteLine("7 - Remover exercício");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void AdicionarExercicio()
    {
        Console.WriteLine("\n--- Novo Exercício ---");

        string nome;
        do
        {
            Console.Write("Nome do exercício: ");
            nome = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(nome));

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        double carga;
        while (true)
        {
            Console.Write("Carga (kg): ");
            if (double.TryParse(Console.ReadLine(), out carga) && carga >= 0) break;
            Console.WriteLine("Erro: Carga deve ser um número >= 0.");
        }

        int reps;
        while (true)
        {
            Console.Write("Repetições: ");
            if (int.TryParse(Console.ReadLine(), out reps) && reps >= 1) break;
            Console.WriteLine("Erro: Repetições devem ser um número inteiro >= 1.");
        }

        nomes.Add(nome);
        gruposMusculares.Add(grupo);
        cargas.Add(carga);
        repeticoes.Add(reps);

        Console.WriteLine("Exercício adicionado com sucesso!");
    }

    static void ListarExercicios()
    {
        if (nomes.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.");
            return;
        }

        Console.WriteLine("\n--- Lista de Exercícios ---");
        for (int i = 0; i < nomes.Count; i++)
        {
            Console.WriteLine($"{nomes[i]} - {gruposMusculares[i]} - {cargas[i]}kg - {repeticoes[i]} reps");
        }
    }

    static void BuscarPorNome()
    {
        Console.Write("Digite o nome para buscar: ");
        string busca = Console.ReadLine();

       
        var index = nomes.FindIndex(x => x.Equals(busca, StringComparison.OrdinalIgnoreCase));

        if (index != -1)
        {
            Console.WriteLine($"\nEncontrado: {nomes[index]} | Grupo: {gruposMusculares[index]} | Carga: {cargas[index]}kg | Reps: {repeticoes[index]}");
        }
        else
        {
            Console.WriteLine("Exercício não encontrado.");
        }
    }

    static void FiltrarPorGrupo()
    {
        Console.Write("Digite o grupo muscular: ");
        string grupo = Console.ReadLine();


        var indicesFiltrados = Enumerable.Range(0, nomes.Count)
            .Where(i => gruposMusculares[i].Equals(grupo, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (indicesFiltrados.Any())
        {
            Console.WriteLine($"\nExercícios de {grupo}:");
            indicesFiltrados.ForEach(i => Console.WriteLine($"- {nomes[i]}"));
        }
        else
        {
            Console.WriteLine("Nenhum exercício encontrado para este grupo.");
        }
    }

    static void CalcularCargaTotal()
    {
        // LINQ para somar as cargas
        double total = cargas.Sum();
        Console.WriteLine($"\nA carga total do treino é: {total} kg");
    }

    static void ExibirMaisPesado()
    {
        if (!cargas.Any())
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

      
        double maiorCarga = cargas.Max();
        int index = cargas.IndexOf(maiorCarga);

        Console.WriteLine($"\nExercício mais pesado: {nomes[index]} com {cargas[index]} kg");
    }

    static void RemoverExercicio()
    {
        Console.Write("Digite o nome do exercício a remover: ");
        string busca = Console.ReadLine();

        int index = nomes.FindIndex(x => x.Equals(busca, StringComparison.OrdinalIgnoreCase));

        if (index != -1)
        {
            nomes.RemoveAt(index);
            gruposMusculares.RemoveAt(index);
            cargas.RemoveAt(index);
            repeticoes.RemoveAt(index);
            Console.WriteLine("Exercício removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Exercício não encontrado.");
        }
    }
}
