const mysql = require("mysql");
const mysqlconnection = mysql.createConnection({
    host: 'localhost',
    port: 3306,
    user: 'root',
    password: '',
    database: 'schoolfeedback'
  });

  module.exports.mysqlconnection = mysqlconnection;