const express = require('express');
const bcrypt = require('bcrypt');
var router = express.Router();
const db = require('../db');
const jwt = require('jsonwebtoken')

require('dotenv').config()


/* GET users listing. */
router.get('/', (req, res) => {
    db.mysqlconnection.query("SELECT * FROM utente", function (error, result) {
        if (error) {
            res.status(500).send("Error: " + error.message);
        }
        res.json(result);
    })
});

/* POST create user. */
router.post('/create', async (req, res) => {
    try {
        const hashedPassword = await bcrypt.hash(req.body.password, 10);
        db.mysqlconnection.query('INSERT INTO utente VALUES(null, ?, ?, ?, ?)', [req.body.username, hashedPassword, req.body.nome, req.body.cognome], function (error, result) {
            if (error)
                return res.status(500).send();
            else
                res.status(201).send();
        });
    } catch {
        res.status(500).send();
    }
});

/* POST login. */
router.post('/login', (req, res) => {
    var user = null;
    try {
        new Promise((resolve, reject) => {
            db.mysqlconnection.query("SELECT * FROM utente WHERE username = ?", [req.body.username], (error, result) => {
                console.log(result);
                if (error)
                    reject(error);
                if (result[0] == null){
                    reject(new Error("user not found"));
                }
                else {
                    user = Object.assign({}, result[0]);
                    resolve(user);
                }
            });
        })
        .then((user) => {
            bcrypt.compare(req.body.password, user.password, (err, ris) => {
                if(err){
                    res.status(403).json("Not Allowed");
                }
                if(ris){
                    const accessToken = generateAccessToken(user.username);
                    res.status(200).json({ accessToken : accessToken});
                }
                else{
                    res.status(401).json("password don't mach");
                }
            })
        })
        .catch((err)=>{
            console.log("catch  "+ err.message);
            res.status(403).send(err.message);
        });
    } catch {
        res.status(500).send();
    }
});

function generateAccessToken(user) {
    var userObj = { username : user};
    return jwt.sign(userObj, process.env.ACCESS_TOKEN_SECRET);;
  }


module.exports = router;