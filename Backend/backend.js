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
        database: 's4_project_M'
    })

    connection.connect();

    connection.query('SELECT users.username,users.email,users.password FROM users', function (err, rows, fields) {
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
        database: 's4_project_M'
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
        database: 's4_project_M'
    })

    connection.connect();

    connection.query('INSERT INTO user VALUES (NULL, "'+req.body.name+'","'+ req.body.password +'","' + req.body.birthdate + '");', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni");
    })

    connection.end();
  })


  let dt=new Date();
  let teljesdat=dt.getFullYear()+"-"+(dt.getMonth()+1)+"-"+dt.getDate();
  app.post('/score_upload', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 's1.siralycore.hu:3306',
        user: 'u4_9RLV2vf67y',
        password: 'Ir^TAk@I^WuhckV=xMuBXKNf',
        database: 's4_Project_M'
    })

    connection.connect();

    connection.query('INSERT INTO statisztika VALUES (NULL, "'+req.body.name+'","'+ req.body.point +'","' + req.body.death + '","' + req.body.maptime + '","' + teljesdat + ');', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni a statisztikába");
    })

    connection.end();
  })

  app.post('/user_create', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '', 
        database: 's4_project_M'
    })

    connection.connect();

    connection.query('INSERT INTO users VALUES (NULL, "'+req.body.username+'","'+ req.body.email +'","' + req.body.password + '", NULL, NULL);', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni");
    })

    connection.end();
  })

  app.post('/deleteErtekeles', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '', 
        database: 's4_project_M'
    })

    connection.connect();

    connection.query('DELETE FROM ertekeles where ertekeles_id=' + req.body.bevitel1 , function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");  
    res.send("Sikeres törlés");
    })

    connection.end();
  })




app.listen(3000, () => console.log('started and listening'));

