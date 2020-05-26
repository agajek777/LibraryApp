"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

var timer;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    timer = 0;
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    if (user == "" || message == "") {
        toastr.error("Fill in the Message field");
        return;
    }
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

    document.getElementById("sendButton").disabled = true;
    setTimeout(function () {
        document.getElementById("sendButton").disabled = false;
    }, 10000);

});