namespace CocinaNadia.Presentation;

// Pantalla que se muestra en lugar de cerrar la app cuando ocurre un error
// no controlado. El texto se puede seleccionar y copiar a mano (Editor de
// solo lectura), y además hay un botón que copia todo al portapapeles.
public class ErrorPage : ContentPage
{
    public ErrorPage(string mensajeError)
    {
        var editorError = new Editor
        {
            Text = mensajeError,
            IsReadOnly = true,
            AutoSize = EditorAutoSizeOption.TextChanges,
            FontFamily = "monospace"
        };

        var botonCopiar = new Button { Text = "Copiar al portapapeles" };
        botonCopiar.Clicked += async (_, _) => await Clipboard.SetTextAsync(mensajeError);

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 15,
                Children =
                {
                    new Label
                    {
                        Text = "Ocurrió un error inesperado",
                        FontSize = 20,
                        FontAttributes = FontAttributes.Bold
                    },
                    botonCopiar,
                    editorError
                }
            }
        };
    }
}
