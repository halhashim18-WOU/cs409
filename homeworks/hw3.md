## CS 409 - Homework 3

### Goals for this assignment

1. Extend your Homework 2 with a new feature
2. Get more practice with the Repository pattern
3. Get more practice with LINQ
4. Learn to use an ApiController to implement your own small API
5. Learn to make a responsive feature using JavaScript and AJAX
6. Learn to implement one of the CRUD functions using both traditional and responsive methods

### Setting

Our Homework 2 was nice and all but the user couldn't do anything.  All the functionality was static data.  Sure, if the information in the database changed it would in fact be reflected on the info page.  But with all this great data let's build something useful.  Pretend we have actual users.  Here's what one of them might have said they want ...

> I follow a bunch of my favorite actors on social media and often see the new shows they're working on.  But I have a hard time finding older shows they were in before I started following them.  Sure, I know about the popular shows they starred in but what about some of the more obscure ones?  I'd like to be able see all the shows they've been in so I can go back and watch some of them.  Maybe discover some great "new to me" shows.  

You begin analyzing these "requirements" and come up with the following:

- The user already knows the actor they're interested in and so likely already know their name
  - They can type in the name and we can search for it easily.  Browsing for an actor's name would be clunky.
  - Once we find the actor, the user can confirm by clicking on it.  If we don't find a match we'll have to tell them that and they can re-enter it
  - Display the matches as links or buttons
- The user wants to see all shows the actor has acted in so they can discover something to watch
  - Display the list of shows under the actor's name
  - in chronological order (for now, a later feature could allow sorting by rating) including title, description, movie or TV show, rating (PG,PG-13,R,...), release year, runtime, and IMDB and TMBD ratings
  - include the director's name as well
- The user follows a lot of actors and so probably wants to quickly look up other actors
  - use JavaScript to make this responsive -- no page loads for the search results or displaying the show

### Feature #1

Build the search feature described above.  Implementation requirements:

- Put it on the Home page
- Have a search bar that takes an actor's name
- Produce search results under the search bar with all possible matches.  If there is no result, notify the user, otherwise, show each result in a clickable format.  You can decide how to match names; try to make it work well.  This should be done with JavaScript.
- When the user clicks on an actor's name, use JavaScript to retrieve all their shows and display as described above. 
- Both the search and the shows should be accessed as REST endpoints on your server.  Use two ApiControllers to build these endpoints: one for Actors and one for Shows.  These should be asynchronous GET requests and should be handled at endpoints /api/search/actor and /api/actor/shows.
- Searching for and selecting a different actor should be super fast and easy.
- Build the back-end first and test it with Swagger or Postman; then build the front-end.

### Setting #2

This site has administrators who need to be able to keep the database updated.  One thing they need to be able to do is to add new actors into the database.  A new actor has a name and a Just Watch ID and both are required.  We must prevent duplicates in the database.  An actor is a duplicate if they have the same JustWatchID.  Since this is not a PK in the database you'll have to do this manually.  If the admin submits a valid create request for an actor who is already in the database, the request should replace the data that was previously there.  Therefore, this functionality can also be used to update the name of an actor, which is not an uncommon occurrence.

### Feature #2

Build a Create functionality starting at /admin/actor/create where an admin can create a new Person.  We don't have a role-based login system yet so technically anyone can access this page and add an actor.  This should be implemented using the "standard" Create workflow with traditional page loads.  Confirm you have it working correctly by using the search functionality to search for this new actor.  They should have zero shows since they're new; or if it's an update you should be able to find them through the new name.

You basically have 2 options for implementing this and both are fine: 1) Use a single create form and have it work for both create and update; or 2) keep them separate and have the traditional create page/form and edit page/form.  For the latter you'll need either the PK Id or the JustWatchId.  Assume the user already has one or both of those.
Feature #3

Duplicate the create actor feature by re-implementing it using JavaScript and a REST API endpoint.  Put this page at /admin/person/create.  It has only a single page that is responsive, and uses an asynchronous PUT request to /api/actor.  Display the result of the request to the user so they can tell if it was successful or if there were errors.

### Turn it in

This assignment is due the weekend of November 27-29. Not a hard deadline, but I want to be able to review by the start of the following week. Stay engaged with me over email as questions come up. Chat with me on Discord or schedule time as you need help.