//dipendenze
require('dotenv').config()
var express = require('express');
var path = require('path');
var http = require('http');
const morgan = require('morgan');


var app = express();


//impostazione della porta
var port = "3000";
app.set('port', port);

//importazione delle routes
var indexRouter = require('./routes/index');
var feedbacksRouter = require('./routes/feedbacks');
var servicesRouter = require('./routes/services');


app.use(morgan('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', indexRouter);
app.use('/api/feedbacks', feedbacksRouter);
app.use('/api/services', servicesRouter);

//Avvio server
var server = http.createServer(app);
server.listen(port);

server.on('error', onError);
server.on('listening', onListening);




function onListening() {
    console.info("API REST Listening on port " + port);
}

function onError(error) {
  if (error.syscall !== 'listen') {
    throw error;
  }

  var bind = typeof port === 'string'
    ? 'Pipe ' + port
    : 'Port ' + port;

  // handle specific listen errors with friendly messages
  switch (error.code) {
    case 'EACCES':
      console.error(bind + ' requires elevated privileges');
      process.exit(1);
      break;
    case 'EADDRINUSE':
      console.error(bind + ' is already in use');
      process.exit(1);
      break;
    default:
      throw error;
  }
}

module.exports = app;

