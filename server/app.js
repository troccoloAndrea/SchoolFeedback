var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');
const cors = require("cors");
const jwt = require("express-jwt");
const jwksRsa = require("jwks-rsa");

var indexRouter = require('./routes/index');
var usersRouter = require('./routes/users');

var app = express();

app.use(cors({ origin: 'http://localhost:3000' }));

const authConfig = {
    domain: "dev-juk8pha8.eu.auth0.com",
    audience: "http://localhost:9000"
};

const checkJwt = jwt({
    secret: jwksRsa.expressJwtSecret({
        cache: true,
        rateLimit: true,
        jwksRequestsPerMinute: 5,
        jwksUri: `https://${authConfig.domain}/.well-known/jwks.json`
    }),

    audience: authConfig.audience,
    issuer: `https://${authConfig.domain}/`,
    algorithm: ["RS256"]
});

app.use(checkJwt);



app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', checkJwt ,indexRouter);
app.use('/users', usersRouter);

app.get("/api/external", checkJwt, (req, res) => {
    res.send({
      msg: "Your Access Token was successfully validated!"
    });
  });

module.exports = app;


