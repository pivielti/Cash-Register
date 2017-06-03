const ASSETS_PUBLIC_PATH = '/assets/';
const STYLES_PUBLIC_PATH = '/styles/';
const SCRIPTS_PUBLIC_PATH = '/scripts/';

const path = require('path');
const ExtractTextPlugin = require("extract-text-webpack-plugin");

module.exports = {
    entry: ['./App/index.js'],
    output: {
        filename: 'scripts/bundle.js',
        publicPath: SCRIPTS_PUBLIC_PATH,
        path: path.resolve(__dirname, 'wwwroot')
    },
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.js',
            'vue-router$': 'vue-router/dist/vue-router.js',
        }
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        // Since sass-loader (weirdly) has SCSS as its default parse mode, we map
                        // the "scss" and "sass" values for the lang attribute to the right configs here.
                        // other preprocessors should work out of the box, no loader config like this necessary.
                        'scss': 'vue-style-loader!css-loader!sass-loader',
                        'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
                    }
                    // other vue-loader options go here
                }
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: /node_modules/
            },
            {
                test: /\.(css|scss)$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader",
                    use: "css-loader!sass-loader"
                })
            },
            {
                test: /\.(png|jpg|gif|svg)$/,
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]?[hash]',
                    publicPath: ASSETS_PUBLIC_PATH
                }
            }
        ]
    },
    plugins: [
        new ExtractTextPlugin({
            filename: "styles/bundle.css",
            publicPath: STYLES_PUBLIC_PATH,
            disable: false,
            allChunks: true
        }),
    ]
};