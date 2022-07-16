# QA Automation Exam: “Contact Book” - 19 Jun 2022

## - Automated Appium UI Tests 
    •	Start the app.
    •	Connect to your backend API service.
    •	Search for keyword “steve”.
    •	Assert that “Steve Jobs” is returned as result.

## - Automated Selenium UI Tests 
    •	List contacts and assert that the first contact is “Steve Jobs”.
    •	Find contacts by keyword “albert” and assert that the first result holds “Albert Einstein”.
    •	Find contacts by keyword “invalid2635” and assert that the results are empty.
    •	Try to create a new contact, holding invalid data, and assert an error is shown.
    •	Create a new contact, holding valid data, and assert the new contact is added and is properly listed at the end of the contacts page.

## - Automated API Tests
    •	List contacts and assert that the first contact is “Steve Jobs”.
    •	Find contacts by keyword “albert” and assert that the first result holds “Albert Einstein”.
    •	Find contacts by keyword “missing{randnum}” and assert that the results are empty.
    •	Try to create a new contact, holding invalid data, and assert an error is returned.
    •	Create a new contact, holding valid data, and assert the new contact is added and is properly listed in the contacts list.
