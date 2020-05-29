"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

function getDate() {
    var today = new Date().toISOString().slice(0, 10);
    today += ('T' + Date.prototype.getHours().toString() + ':' + Date.prototype.getMinutes().toString() + Date.prototype.getMinutes().toString());
    return today;
}

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

    var date;
    date = new Date().toISOString().slice(0, 19);

    var data = {
        text: message,
        sent: date,
        appUser: {
            userName: user
        }
    };

    data = JSON.stringify(data);

    var response = fetch('/api/messages', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: data
    }).then(response => response.json()).then(console.log(response));

    document.getElementById("sendButton").disabled = true;
    setTimeout(function () {
        document.getElementById("sendButton").disabled = false;
    }, 10000);

});