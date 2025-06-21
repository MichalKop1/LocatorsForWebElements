using NUnit.Framework;
using Reqnroll;
using SpecFlowTests.Pages;

namespace SpecFlowTests.Steps;

[Binding]
public class IndexPageSteps : BasePageSteps
{
	private IndexPage _indexPage;
	private ServicesPage _servicesPage;

	[Given(@"I am on the '(.*)' page")]
	public void GivenIAmOnTheHomePage(string link)
	{
		_indexPage = new IndexPage(driver);
		_indexPage.Open(link);
		_indexPage.AcceptCookies();
	}

	[When(@"I click on the ""Services"" link")]
	public void WhenIClickOnTheServicesLink()
	{
		_servicesPage = _indexPage.SelectServices();
	}

	[When(@"I click on the ""ARTIFICIAL INTELLIGENCE"" link")]
	public void WhenIClickOnTheArtificialIntelligenceLink()
	{
		_servicesPage.ClickArtificialIntelligence();
	}

	[When(@"I click on the ""GENERATIVE AI"" link")]
	public void WhenIClickOnTheGenerativeAiLink()
	{
		_servicesPage.ClickGenerativeAi();
	}

	[When(@"I click on the ""RESPONSIBLE AI"" link")]
	public void WhenIClickOnTheResponsibleAiLink()
	{
		_servicesPage.ClickResponsibleAi();
	}

	[Then(@"I should see the '(.*)' title")]
	public void ThenIShouldSeeTheTitle(string title)
	{
		string actualTitle = _servicesPage.GetServiceTitle().ToUpper();
		Assert.That(actualTitle, Is.EquivalentTo(title));
	}

	
}
