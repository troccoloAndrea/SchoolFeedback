var express = require('express');
var router = express.Router();
const auth = require('../authenticateToken');

/* GET users listing. */
router.get('/', auth.authenticateToken , (req, res) => {
  res.status(200).send('respond with a resource - feedback');
});

module.exports = router;