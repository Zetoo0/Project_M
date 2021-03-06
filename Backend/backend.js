const express = require('express');
const app = express();
app.use(express.json())

app.get('/', (req, res) => {
    res.send('Hello Unity Developers!');
})

let enemy = {
    "name" : "slime",
    "healt" : 20,
    "attack" : 5
}

app.get('/user', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '',
        database: 's4_project_m'
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
        database: 's4_project_m'
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
        database: 's4_project_m'
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
        database: 's4_project_m'
    })

    connection.connect();

    connection.query('INSERT INTO user VALUES (NULL, "'+req.body.name+'",'+ req.body.point + ');', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni");
    })

    connection.end();
  })

  app.post('/statisztika_upload', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
        host: 'localhost',
        user: 'root',
        password: '',
        database: 's4_project_m'
    })

    connection.connect();
    let dt=new Date();
    let teljesdat=dt.getFullYear()+"-"+(dt.getMonth()+1)+"-"+dt.getDate();
    connection.query('INSERT INTO statisztika VALUES (NULL,'+req.body.userID+',"'+req.body.name+'",'+ req.body.point + ',' + req.body.death + ',"' + req.body.maptime + '","' + teljesdat + '",' + req.body.levelId +','+ req.body.partId +');', function (err, rows, fields) {
    if (err) throw err;

    console.log("sikerült");
    res.send("Sikerült felvinni");
    })

    connection.end();
  })

  app.post('/registerCheck', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
      host: 'localhost',
      user: 'root',
      password: '',
      database: 's4_Project_M'
    })
    
    connection.connect()
    //var szoveg= "like '%"+req.body.bevitel1+"%'";
    connection.query("SELECT user_name FROM user WHERE user_name='"+req.body.name + "'", function (err, rows, fields) {
        if (err) throw err
      	if(rows == "")
        {
          res.send("happy")
        }
        else
        {
          res.send("sad");
        }
      })
      connection.end();
  })

  app.post('/register', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
      host: 'localhost',
      user: 'root',
      password: '',
      database: 's4_Project_M'
    })
    
    connection.connect()
    //var szoveg= "like '%"+req.body.bevitel1+"%'";
    connection.query("INSERT INTO user VALUES(NULL,'"+req.body.name+"','"+req.body.password+"')", function (err, rows, fields) {
        if (err) throw err
      	res.send("Sikeres regisztráció!");
        console.log("Regisztrált");
      })
      connection.end();
  })

  //---------------------------

  app.post('/loginCheck', (req, res) => {
    var mysql = require('mysql')
    var connection = mysql.createConnection({
      host: 'localhost',
      user: 'root',
      password: '',
      database: 's4_Project_M'
    })
    
    connection.connect()
    //var szoveg= "like '%"+req.body.bevitel1+"%'";
    connection.query("SELECT user_id FROM user WHERE user_name='"+req.body.name + "' AND user_password='"+req.body.password+"'", function (err, rows, fields) {
        if (err) throw err
      	if(rows == "")
        {
          res.send("sad")
        }
        else
        {
          res.send(rows);
          console.log(rows);
          console.log("Belépés");
        }
      })
      connection.end();
  })

  


app.listen(3000, () => console.log('started and listening.'));