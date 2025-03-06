using System.Diagnostics;

namespace WF.Reflexo
{
    public partial class Form1 : Form
    {
        // variaveis
        // propriedades
        // 2 botoes
        private Button btnIniciar;
        private Button btnAlvo;
        // timer
        private System.Windows.Forms.Timer timer;
        // random
        private Random random;
        // stopwatch
        private Stopwatch stopwatch;
        // construtor tela
        public Form1()
        {
            InitializeComponent();
            // determina o título da tela
            this.Text = "Reflexo";
            // determina a altura e largura da tela
            this.Size = new Size(400, 400);
            // abre a tela centralizada
            this.StartPosition = FormStartPosition.CenterScreen;
            // determina os botões da tela
            btnIniciar = new Button()
            {
                Text = "Iniciar",
                Size = new Size(100, 50)
            };
            btnIniciar.Click += IniciarJogo;
            // adiciona o botão na tela
            this.Controls.Add(btnIniciar);

            // cria o btnAlvo
            btnAlvo = new Button()
            {
                Size = new Size(100, 100),
                BackColor = Color.Red,
                Visible = false
            };
            btnAlvo.Click += btnAlvoClick;
            // adiciona o botão na tela, mas oculto
            this.Controls.Add(btnAlvo);

            // timer
            timer = new System.Windows.Forms.Timer();
            timer.Tick += MostrarBotaoAlvo;

            // random
            random = new Random();
            stopwatch = new Stopwatch();

        } // Fim construtor

        // IniciarJogo
        private void IniciarJogo(object sender, EventArgs e)
        {
            // desabilita o botão
            btnIniciar.Enabled = false;
            IniciarNovaRodada();
        }
        private void IniciarNovaRodada()
        {
            // determina um timer aleatorio entre 1 e 3 segundos
            timer.Interval = random.Next(1000, 3000);
            timer.Start();
        }
        private void MostrarBotaoAlvo(object sender, EventArgs e)
        {
            timer.Stop(); // parar o timer ao clicar no botão alvo
            // gerar valores aleatorios para a posicao do botão Alvo
            int x = random.Next(50, this.ClientSize.Width - 70);
            int y = random.Next(50, this.ClientSize.Height - 70);
            btnAlvo.Location = new Point(x, y); // define a posicao do botão ALVO 
            btnAlvo.Visible = true; // exibe o botão Alvo na tela
            stopwatch.Restart(); // reiniciar o cronometro
        }
        private void btnAlvoClick(object sender, EventArgs e)
        {
            int novaLarguraBotao = btnAlvo.Size.Width - 10;
            int novaAlturaBotao = btnAlvo.Size.Height - 10;

            btnAlvo.Size = new Size(novaLarguraBotao, novaAlturaBotao);

            stopwatch.Stop(); // para o cronometro
            btnAlvo.Visible = false; // oculta o botão alvo
            MessageBox.Show($"Tempo de reação: {stopwatch.ElapsedMilliseconds}", "Res");
            Task.Delay(500).
                ContinueWith(_ => IniciarNovaRodada(),
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
