public class GerenciadorCamposFormulario {
    public void HabilitarCamposVenda(Entry descontoEntry, Entry clienteEntry, Entry produtoEntry, Entry quantidadeEntry) {
        descontoEntry.IsEnabled = true;
        clienteEntry.IsEnabled = true;
        produtoEntry.IsEnabled = true;
        quantidadeEntry.IsEnabled = true;
    }

    public void HabilitarCamposCliente(
        Entry idEntry, Entry clNomeEntry, Entry cpfCnpjEntry, Entry clRgEntry, Entry clDnEntry,
        Entry ruaEntry, Entry numeroEntry, Entry complementarEntry, Entry cepEntry,
        Entry bairroEntry, Entry estadoEntry, Entry cidadeEntry,
        RadioButton radioButtonPF, RadioButton radioButtonPJ) {
        idEntry.IsEnabled = true;
        clNomeEntry.IsEnabled = true;
        cpfCnpjEntry.IsEnabled = true;
        clRgEntry.IsEnabled = true;
        clDnEntry.IsEnabled = true;
        ruaEntry.IsEnabled = true;
        numeroEntry.IsEnabled = true;
        complementarEntry.IsEnabled = true;
        cepEntry.IsEnabled = true;
        bairroEntry.IsEnabled = true;
        estadoEntry.IsEnabled = true;
        cidadeEntry.IsEnabled = true;
        radioButtonPF.IsEnabled = true;
        radioButtonPJ.IsEnabled = true;
    }
}
