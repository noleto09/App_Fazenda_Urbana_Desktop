public class FramesPaginaVendas {
    public StackLayout CriarStackLayout(string texto) {
        return new StackLayout {
            Orientation = StackOrientation.Horizontal,
            Children = {
                new Label { Text = texto, WidthRequest = 100 }
            }
        };
    }

    public Frame CriarFrame(StackLayout layout) {
        return new Frame {
            Content = layout,
            BackgroundColor = Colors.White,
            BorderColor = Colors.Gainsboro,
            CornerRadius = 0,
            Padding = new Thickness(5, 2),
            Margin = new Thickness(0, 0)
        };
    }

    public void AdicionarFramesPaginaVendas(
        StackLayout productStackLayout, StackLayout linhaQuantidade,
        StackLayout linhaPrecoUn, StackLayout linhaDesconto,
        StackLayout linhaValorTotal,
        string produtoDigitado, string quantidadeDigitado,
        string precoUnDigitado, string descontoDigitado,
        string valorTotalDigitado) {
        var productFrame = CriarFrame(CriarStackLayout(produtoDigitado));
        var quantidadeFrame = CriarFrame(CriarStackLayout(quantidadeDigitado));
        var precoUnFrame = CriarFrame(CriarStackLayout(precoUnDigitado));
        var descontoFrame = CriarFrame(CriarStackLayout(descontoDigitado));
        var valorTotalFrame = CriarFrame(CriarStackLayout(valorTotalDigitado));

        // Adiciona os frames aos StackLayouts apropriados
        productStackLayout.Children.Add(productFrame);
        linhaQuantidade.Children.Add(quantidadeFrame);
        linhaPrecoUn.Children.Add(precoUnFrame);
        linhaDesconto.Children.Add(descontoFrame);
        linhaValorTotal.Children.Add(valorTotalFrame);
    }
}
