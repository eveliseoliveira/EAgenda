using EAgenda.Compartilhado;
using EAgenda.ConsoleApp.Compartilhado;
using EAgenda.ModuloContato;
using EAgenda.ModuloCompromisso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAgenda.ModuloTarefa;

namespace EAgenda.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Compromisso> repositorioCompromisso;
        private readonly IRepositorio<Contato> repositorioContato;
        private readonly TelaCadastroContato telaCadastroContato;

        public TelaCadastroCompromisso(
            Notificador notificador,
            IRepositorio<Compromisso> repositorioCompromisso,
            IRepositorio<Contato> repositorioContato,
            TelaCadastroContato telaCadastroContato) : base("Cadastro de Compromissos")
        {
            this.notificador = notificador;
            this.repositorioCompromisso = repositorioCompromisso;
            this.repositorioContato = repositorioContato;
            this.telaCadastroContato = telaCadastroContato;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Registrar Compromisso");
            Console.WriteLine("Digite 2 para Editar Compromisso");
            Console.WriteLine("Digite 3 para Excluir Compromisso");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Compromisso em Aberto");
            Console.WriteLine("Digite 6 para Devolver um empréstimo");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void RegistrarCompromisso()
        {
            MostrarTitulo("Inserindo novo Compromisso");

            Contato contatoSelecionado = ObtemContato();

            if (contatoSelecionado == null)
            {
                notificador.ApresentarMensagem("Nenhum contato selecionado", TipoMensagem.Erro);
                return;
            }

             if (contatoSelecionado.TemCompromissoEmAberto())
             {
                 notificador.ApresentarMensagem("Este contato já tem um compromisso em aberto.", TipoMensagem.Erro);
                 return;
             }

            Compromisso compromisso = ObtemCompromisso(contatoSelecionado);

            string statusValidacao = repositorioCompromisso.Inserir(compromisso);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarCompromisso()
        {
            MostrarTitulo("Editando Compromissos");

            bool temCompromissosCadastrados = VisualizarCompromissos("Pesquisando");

            if (temCompromissosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum compromisso cadastrado para poder editar", TipoMensagem.Atencao);
                return;
            }
            int numeroCompromisso = ObterNumeroCompromisso();

            Contato contatoSelecionado = ObtemContato();

            Compromisso compromissoAtualizado = ObtemCompromisso(contatoSelecionado);

            bool conseguiuEditar = repositorioCompromisso.Editar(x => x.numero == numeroCompromisso, compromissoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Compromisso editado com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirCompromisso()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissosCadastrados = VisualizarCompromissos("Pesquisando");

            if (temCompromissosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum compromisso cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroCompromisso();

            bool conseguiuExcluir = repositorioCompromisso.Excluir(x => x.numero == numeroCompromisso);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Compromisso excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarCompromissos(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Compromissos");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum compromisso disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarCompromissosEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Empréstimos em Aberto");

            List<Compromisso> compromissos = repositorioCompromisso.Filtrar(x => x.EstaAberto);

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum compromisso em aberto disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        #region Métodos privados
        private Compromisso ObtemCompromisso(Contato contato)
        {
            Compromisso novoCompromisso = new Compromisso(contato);

            return novoCompromisso;
        }

        private Contato ObtemContato()
        {
            bool temContatosDisponiveis = telaCadastroContato.VisualizarRegistros("Pesquisando");

            if (!temContatosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponível para cadastrar compromisso.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do contato que irá pegar o compromisso: ");
            int numeroContatoCompromisso = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Contato contatoSelecionado = repositorioContato.SelecionarRegistro(x => x.numero == numeroContatoCompromisso);
            return contatoSelecionado;
        }

        private int ObterNumeroCompromisso()
        {
            int numeroCompromisso;
            bool numeroCompromissoEncontrado;

            do
            {
                Console.Write("Digite o número do compromisso que deseja selecionar: ");
                numeroCompromisso = Convert.ToInt32(Console.ReadLine());

                numeroCompromissoEncontrado = repositorioCompromisso.ExisteRegistro(x => x.numero == numeroCompromisso);

                if (!numeroCompromissoEncontrado)
                    notificador.ApresentarMensagem("Número do compromisso não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (!numeroCompromissoEncontrado);

            return numeroCompromisso;
        }

        public static implicit operator TelaCadastroCompromisso(TelaCadastroTarefa v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
