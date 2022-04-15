using EAgenda.Compartilhado;
using EAgenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAgenda.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Tarefa> repositorioTarefa;

        //public string item;

        public TelaCadastroTarefa(IRepositorio<Tarefa> repositorioTarefa, Notificador notificador)
            : base("Cadastro de Contatos")
        {
            this.repositorioTarefa = repositorioTarefa;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }


        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo nova tarefa");

            Tarefa novaTarefa = ObterTarefa();

            string statusValidacao = repositorioTarefa.Inserir(novaTarefa);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            Tarefa contatoAtualizado = ObterTarefa();

            bool conseguiuEditar = repositorioTarefa.Editar(x => x.numero == numeroTarefa, contatoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastrados = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma tarefa cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            bool conseguiuExcluir = repositorioTarefa.Excluir(x => x.numero == numeroTarefa);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Tarefa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Tarefas");

            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

       /* public void QuantidadeItem()
        {
            Console.WriteLine("Digite a quantidade de itens: ");
            int quantidadeItem = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < quantidadeItem; i++)
            {
                Console.Write("Digite o item #" + i + ": " + item);
                item = Console.ReadLine();
            }
        }*/

        #region Métodos privados
        private int ObterNumeroTarefa()
        {
            int numeroTarefa;
            bool numeroTarefaEncontrado;

            do
            {
                Console.Write("Digite o número da tarefa que deseja selecionar: ");
                numeroTarefa = Convert.ToInt32(Console.ReadLine());

                numeroTarefaEncontrado = repositorioTarefa.ExisteRegistro(x => x.numero == numeroTarefa);

                if (numeroTarefaEncontrado == false)
                    notificador.ApresentarMensagem("Número da tarefa não encontrado, digite novamente.", TipoMensagem.Atencao);

            } while (numeroTarefaEncontrado == false);
            return numeroTarefa;
        }

        private Tarefa ObterTarefa()
        {
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a data de inicio: ");
            string dataInicio = Console.ReadLine();

            Console.Write("Digite a data final: ");
            string dataFinal = Console.ReadLine();

            //QuantidadeItem();

            Console.Write("Digite o item: ");
            string item = Console.ReadLine();

            Tarefa tarefa = new(titulo, dataInicio, dataFinal, item);

            return tarefa;
        }
        #endregion
    }
}
