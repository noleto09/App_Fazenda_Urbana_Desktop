public class FramesPaginaVendas {
    private int currentRedLineIndex = 0;

    // Método para criar um StackLayout com o texto fornecido
    public StackLayout CriarStackLayout(string texto) {
        return new StackLayout {
            Orientation = StackOrientation.Horizontal,
            Children = {
                new Label { Text = texto, WidthRequest = 100 }
            }
        };
    }

    // Método para criar um Frame com o layout e a cor de fundo (cinza ou branco)
    public Frame CriarFrame(StackLayout layout, bool isLightGray) {
        return new Frame {
            Content = layout,
            BackgroundColor = isLightGray ? Colors.LightGray : Colors.White,
            BorderColor = Colors.Gainsboro,
            CornerRadius = 0,
            Padding = new Thickness(5, 2),
            Margin = new Thickness(0, 0)
        };
    }

    // Método para adicionar Frames à página de vendas com comportamento interativo
    public void AdicionarFramesPaginaVendas(
        StackLayout productStackLayout, StackLayout linhaQuantidade,
        StackLayout linhaPrecoUn, StackLayout linhaDesconto,
        StackLayout linhaValorTotal,
        string produtoDigitado, string quantidadeDigitado,
        string precoUnDigitado, string descontoDigitado,
        string valorTotalDigitado) {
        // Verificar se a linha atual deve ser colorida
        bool isRed = productStackLayout.Children.Count == currentRedLineIndex;

        // Criar os Frames para produto, quantidade, preço unitário, desconto e valor total
        var productFrame = CriarFrame(CriarStackLayout(produtoDigitado), isRed);
        var quantidadeFrame = CriarFrame(CriarStackLayout(quantidadeDigitado), isRed);
        var precoUnFrame = CriarFrame(CriarStackLayout(precoUnDigitado), isRed);
        var descontoFrame = CriarFrame(CriarStackLayout(descontoDigitado), isRed);
        var valorTotalFrame = CriarFrame(CriarStackLayout(valorTotalDigitado), isRed);

       
        
        // Adicionar as Frames aos StackLayouts correspondentes
        productStackLayout.Children.Add(productFrame);
        linhaQuantidade.Children.Add(quantidadeFrame);
        linhaPrecoUn.Children.Add(precoUnFrame);
        linhaDesconto.Children.Add(descontoFrame);
        linhaValorTotal.Children.Add(valorTotalFrame);
    }
}
