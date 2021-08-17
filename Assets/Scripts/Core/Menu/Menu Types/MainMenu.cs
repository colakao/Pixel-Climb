using Core.Menu.MenuSystem;

namespace Core
{
	namespace Menu
	{
		namespace MenuTypes
		{
			public class MainMenu : SimpleMenu<MainMenu>
			{
				public void OnPlayPressed()
				{
					GameManager.Instance.ReloadNormal();
					Close();

					if (!GameManager.Instance.tutorialSeen)
					{
						GameManager.Instance.tutorialSeen = true;
						GameManager.Instance.Paused();

						TutorialMenu.Show();
					}
					else
						GameMenu.Show();
				}

				public void OnCreditsPressed()
				{
					CreditsMenu.Show();
				}

				public void OnOptionsPressed()
				{
					OptionsMenu.Show();
				}

				public override void OnBackPressed()
				{
					PromptExitMenu.Show();
				}

				private void OnEnable()
				{
					GameManager.Instance.LoadPlayer();					
				}
			}

		}
	}
}
