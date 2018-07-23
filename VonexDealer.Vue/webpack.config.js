const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [{
        stats: { modules: false },
        entry: { 'main': './ClientApp/boot-app.js' },
        resolve: {
            extensions: ['.js', '.vue'],
            alias: {
                'vue$': 'vue/dist/vue',
                'components': path.resolve(__dirname, './ClientApp/components'),
                'views': path.resolve(__dirname, './ClientApp/views'),
                'utils': path.resolve(__dirname, './ClientApp/utils'),
                'api': path.resolve(__dirname, './ClientApp/store/api')
            }
        },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: '/dist/'
        },
        module: {
            rules: [
                { test: /\.vue$/, include: /ClientApp/, use: 'vue-loader' },
                {
                    test: /\.(js|vue)$/,
                    loader: require.resolve('string-replace-loader'),
                    include: /ClientApp/,
                    query: {
                        multiple: [{
                            search: '__apiURL__',
                            replace: isDevBuild ? 'localhost:44324' : 'sogapi.vonex.com.au'
                        },
                            {
                                search: '__URL__',
                                replace: isDevBuild ? 'localhost:44309' : 'sog.vonex.com.au'
                            },
                            {
                                search: '__ClientID__',
                                replace: isDevBuild ? 'CdVxFoQZuhiu6nK81svctQsYGuL9mzcG' : 'q96Spcb44igpm1yCHocTE3rH8HECuKax'
                            }]
                        
                    }
                },
                { test: /\.js$/, include: /ClientApp/, use: 'babel-loader' },
               
                //{ test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader' }) },
                { test: /\.css$/, use: ['style-loader', 'css-loader']  },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' },
                {
                    test: /\.(eot|woff|woff2|svg|ttf)([\?]?.*)$/, loader: "file-loader"
                }
            ]
        },
        plugins: [
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            })
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
           
        ] : [
                // Plugins that apply in production builds only
                new webpack.optimize.UglifyJsPlugin({
                    test: /\.(js)$/,
                    sourceMap: true,
                    uglifyOptions: { ecma: 8 },
                }),
               // new ExtractTextPlugin('site.css')
            ])
    }];
};
