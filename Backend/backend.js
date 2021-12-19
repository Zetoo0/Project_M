const express = require('express');
const app = express();
app.use(express.json())

app.get('/', (req, res) => {
    res.send('Hello Unity Developers!');
})


app.get('/user', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '',
        database: 'projectGame'
    })

    connection.connect();

    connection.query('SELECT * FROM user', function (err, rows, fields) {
    if (err) throw err;

    res.send(rows);
    })

    connection.end();
  })

  app.post('/enemy_post', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '',
        database: 'projectGame'
    })

    connection.connect();

    connection.query('INSERT INTO enemy VALUES (NULL, "'+req.body.name+'",'+ req.body.health +',' + req.body.attack + ');', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni");
    })

    connection.end();
  })

  app.post('/user_create', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '',
        database: 'projectGame'
    })

    connection.connect();

    connection.query('INSERT INTO user VALUES (NULL, "'+req.body.name+'","'+ req.body.password +'","' + req.body.birthdate + '");', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni");
    })

    connection.end();
  })

  app.post('/score_upload', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '',
        database: 'projectGame'
    })

    connection.connect();

    connection.query('INSERT INTO score VALUES (NULL, "'+req.body.point+'","'+ req.body.name +'","' + req.body.maptime + '");', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni");
    })

    connection.end();
  })




app.listen(3000, () => console.log('started and listening.'));

