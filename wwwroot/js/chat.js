"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("TwoArgsEchoMethod", function (user, message) {
  var li = document.createElement("li");
  document.getElementById("messagesList").appendChild(li);
  // We can assign user-supplied strings to an element's textContent because it
  // is not interpreted as markup. If you're assigning in any other way, you
  // should be aware of possible script injection concerns.
  li.textContent = `${user}: ${message}`;
});

connection.on("ReceiveMessage", function (user, message) {
  var li = document.createElement("li");
  document.getElementById("messagesList").appendChild(li);
  li.textContent = `${user}: ${message}`;
});

connection
  .start()
  .then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("connectionId").textContent =
      connection.connectionId;
  })
  .catch(function (err) {
    return console.error(err.toString());
  });

document
  .getElementById("sendButton")
  .addEventListener("click", function (event) {
    var user = document.getElementById("username").value;
    var message = document.getElementById("message").value;
    var recepientConnectionId = document.getElementById(
      "recepientConnectionId"
    ).value;
    connection
      .invoke("SendMessage", user, message, recepientConnectionId)
      .catch(function (err) {
        return console.error(err.toString());
      });
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user}: ${message}`;
    event.preventDefault();
  });

document
  .getElementById("clearButton")
  .addEventListener("click", function (event) {
    document.getElementById("messagesList").innerHTML = "";
    event.preventDefault();
  });
