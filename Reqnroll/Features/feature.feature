Feature: Validate Navigation to Services Section

  Scenario Outline: Navigate to Services Section

	Given I am on the <link> page
	When I click on the "Services" link
		And I click on the "ARTIFICIAL INTELLIGENCE" link
		And I click on the "GENERATIVE AI" link
	Then I should see the <title> title

	Examples:
		| link                      | title         |
		| https://www.epam.com/     | GENERATIVE AI |

Scenario Outline: Navigate to Responsible AI Section

    Given I am on the <link> page
    When I click on the "Services" link
		And I click on the "ARTIFICIAL INTELLIGENCE" link
		And I click on the "RESPONSIBLE AI" link
	Then I should see the <title> title

	Examples:
		| link                      | title         |
		| https://www.epam.com/     | RESPONSIBLE AI|