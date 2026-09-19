using Microsoft.Extensions.DependencyInjection;

namespace CocinaNadia.Presentation;

// Se califica "Microsoft.Maui.Controls.Application" con su namespace completo
// porque este proyecto también tiene una capa llamada "CocinaNadia.Application"
// (Clean Architecture) y, al compartir el namespace raíz "CocinaNadia", el
// compilador prioriza esa capa como namespace por sobre la clase de MAUI.
public partial class App : Microsoft.Maui.Controls.Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}