const express = require('express');
const bcrypt = require('bcrypt');
var router = express.Router();
const db = require('../db');



const users = [];

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
        const hashedPassword = await bcrypt.hash(req.body.Password, 12);
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
    try {
        var user = null;
        db.mysqlconnection.query("SELECT * FROM utente WHERE username = ?", [req.body.username], function (error, result) {
            if (error)
                return res.status(500).send("Error: " + error.message);
            if (result.lenght == 0)
                return res.status(400).json('User not found');
            else {
                console.log("sax");
                console.log(result);
                user = Object.assign({}, result[0]);
                if (bcrypt.compare(req.body.password, user.password)) {
                    res.json('Success');
                } else {
                    res.json('not allowed');
                }
            }
        });
        console.log("pronto");

    } catch {
        res.status(500).send();
    }
});



module.exports = router;