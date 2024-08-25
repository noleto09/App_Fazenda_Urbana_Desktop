public class GerenciadorCamposFormulario {
    public void HabilitarCamposVenda(Entry descontoEntry, Entry clienteEntry, Entry produtoEntry, Entry quantidadeEntry) {
        descontoEntry.IsEnabled = true;
        clienteEntry.IsEnabled = true;
        produtoEntry.IsEnabled = true;
        quantidadeEntry.IsEnabled = true;
    }

    public void HabilitarCamposCliente(
       Entry clNomeEntry, Entry cpfCnpjEntry, 
        Entry ruaEntry, Entry numeroEntry, Entry complementarEntry, Entry cepEntry,
        Entry bairroEntry, Entry estadoEntry, Entry cidadeEntry,
        RadioButton radioButtonPF, RadioButton radioButtonPJ) {
        
        clNomeEntry.IsEnabled = true;
        cpfCnpjEntry.IsEnabled = true;
        
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
