const Alexa = require('ask-sdk');

const API_URL = '2c6b4q2jn6.execute-api.ap-northeast-1.amazonaws.com';

async function getUser(userId, firstName) {
    return new Promise((resolve) => {
        var options = {
            host: API_URL,
            port: 443,
            path: '/Prod/user/' + userId + '/' + firstName,
            method: 'GET'
        };

        var https = require('https');

        https.request(options, res => {
            console.log('STATUS: ' + res.statusCode);
            console.log('HEADERS: ' + JSON.stringify(res.headers));
            res.setEncoding('utf8');

            var responseString = '';

            res.on('data', chunk => {
                console.log('BODY: ' + chunk);
                responseString = responseString + chunk;
            });

            res.on('end', () => {
                resolve(JSON.parse(responseString));
            });

        }).end();
    });
}

async function saveUser(user) {
    var jsonString = JSON.stringify(user);

    console.log('jsonString: ' + jsonString);

    var options = {
        host: API_URL,
        port: 443,
        path: '/Prod/user',
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Content-Length': Buffer.byteLength(jsonString)
        }
    };

    var https = require('https');

    var req = https.request(options, function (res) {
        console.log('STATUS: ' + res.statusCode);
        console.log('HEADERS: ' + JSON.stringify(res.headers));
        res.setEncoding('utf8');
        res.on('data', function (chunk) {
            console.log('BODY: ' + chunk);
        });
    });

    req.write(jsonString);
    req.end();
}

async function getWeight(userId, firstName) {

    var options = {
        host: API_URL,
        port: 443,
        path: '/Prod/user/' + userId + '/' + firstName + '/weight/' + new Date(),
        method: 'GET'
    };

    var https = require('https');

    https.request(options, function (res) {
        console.log('STATUS: ' + res.statusCode);
        console.log('HEADERS: ' + JSON.stringify(res.headers));
        res.setEncoding('utf8');
        res.on('data', function (chunk) {
            console.log('BODY: ' + chunk);
            return chunk;
        });
    }).end();
}

const HelpHandler = {
  canHandle(handlerInput) {
    const request = handlerInput.requestEnvelope.request;
    return request.type === 'IntentRequest'
      && request.intent.name === 'AMAZON.HelpIntent';
  },
  handle(handlerInput) {
    return handlerInput.responseBuilder
      .speak(HELP_MESSAGE)
      .reprompt(HELP_REPROMPT)
      .getResponse();
  },
};

const ExitHandler = {
  canHandle(handlerInput) {
    const request = handlerInput.requestEnvelope.request;
    return request.type === 'IntentRequest'
      && (request.intent.name === 'AMAZON.CancelIntent'
        || request.intent.name === 'AMAZON.StopIntent');
  },
  handle(handlerInput) {
    return handlerInput.responseBuilder
      .speak(STOP_MESSAGE)
      .getResponse();
  },
};

const SessionEndedRequestHandler = {
  canHandle(handlerInput) {
    const request = handlerInput.requestEnvelope.request;
    return request.type === 'SessionEndedRequest';
  },
  handle(handlerInput) {
    console.log(`Session ended with reason: ${handlerInput.requestEnvelope.request.reason}`);

    return handlerInput.responseBuilder.getResponse();
  },
};

const ErrorHandler = {
  canHandle() {
    return true;
  },
  handle(handlerInput, error) {
    console.log(`Error handled: ${error.message}`);

    return handlerInput.responseBuilder
      .speak('Sorry, an error occurred.')
      .reprompt('Sorry, an error occurred.')
      .getResponse();
  },
};

const SelectUserIntent = {
   canHandle(handlerInput) {
    const request = handlerInput.requestEnvelope.request;
    return (request.type === 'IntentRequest'
        && request.intent.name === 'SelectUserIntent');
  },
  async handle(handlerInput) {
      const request = handlerInput.requestEnvelope.request;
      const firstName = request.intent.slots.firstName.value;
      console.log(`firstName: ${firstName}`);

      handlerInput.attributesManager.getSessionAttributes().firstName = firstName;

      const userId = handlerInput.requestEnvelope.session.user.userId;
      console.log(`userId: ${userId}`);

      const apiEndpoint = handlerInput.requestEnvelope.context.System.apiEndpoint;
      console.log(`apiEndpoint: ${apiEndpoint}`);
      const apiAccessToken = handlerInput.requestEnvelope.context.System.apiAccessToken;
      console.log(`apiAccessToken: ${apiAccessToken}`);
      const deviceId = handlerInput.requestEnvelope.context.System.device.deviceId;
      console.log(`deviceId: ${deviceId}`);
          
      var speechOutput = "Hello, " + firstName + '. Welcome to ' + SKILL_NAME + '.';

      var user = await getUser(userId, firstName);

      await saveUser(user);

      //var weight = getWeight(userId, firstName);

      //if (!weight) {
      //    speechOutput += ' I don\'t have your weight for 
      //}

    return handlerInput.responseBuilder
      .speak(speechOutput)
      .getResponse();
  },
}

const LaunchHandler = {
    canHandle(handlerInput) {
        const request = handlerInput.requestEnvelope.request;
        return request.type === 'LaunchRequest';
    },
    handle(handlerInput) {
        const speechOutput = "Hello, who am I speaking too?";

        return handlerInput.responseBuilder
            .speak(speechOutput)
            .reprompt(speechOutput)
            .getResponse();
    },
}

const SKILL_NAME = 'My Weigh In';
const HELP_MESSAGE = 'You can say enter my weight, check my progress, or, you can say exit... What can I help you with?';
const HELP_REPROMPT = 'What can I help you with?';
const STOP_MESSAGE = 'Catch you later!';

const skillBuilder = Alexa.SkillBuilders.standard();

exports.handler = skillBuilder
    .addRequestHandlers(
        LaunchHandler,
        SelectUserIntent,
        HelpHandler,
        ExitHandler,
        SessionEndedRequestHandler
  )
  .addErrorHandlers(ErrorHandler)
  .lambda();

