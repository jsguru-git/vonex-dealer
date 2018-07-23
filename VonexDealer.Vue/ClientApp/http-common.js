
import axios from 'axios';


export const HTTP = axios.create({

    baseURL: 'https://__apiURL__/api/',
    headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, PUT, POST, DELETE, OPTIONS',
        'xsrfCookieName': 'XSRF-TOKEN', // default
        // `xsrfHeaderName` is the name of the http header that carries the xsrf token value
        'xsrfHeaderName': 'X-XSRF-TOKEN', // default

      // Authorization: 'Bearer ' + token


    }
})


