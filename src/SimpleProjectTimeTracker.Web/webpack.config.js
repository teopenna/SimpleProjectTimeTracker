const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const bundleOutputDir = './wwwroot/dist';

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [{
        stats: { modules: false },
        mode: isDevBuild ? 'development' : 'production',
        entry: { 'main': './ClientApp/boot.js'},
        resolve: { extensions: ['.js', '.jsx'] },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: 'dist/'
        },
        module: {
            rules: [
                {
                    test: /\.(js|jsx)$/,
                    exclude: /(node_modules|bower_components)/,
                    use: {
                        loader: "babel-loader",
                        options: {
                            presets: ["babel-preset-env", "react", "stage-2"],
                            plugins: ["react-hot-loader/babel"]
                        }
                    }
                },
                {
                    test: /\.css$/,
                    use: isDevBuild
                        ? ["style-loader", "css-loader"]
                        : [MiniCssExtractPlugin.loader, "css-loader?minimize"]
                },
                {
                    test: /\.(png|jpg|jpeg|gif)$/,
                    use: "url-loader?limit=25000"
                },
                {
                    test: /\.(eot|otf|svg|ttf|woff|woff2)$/,
                    use: "file-loader"
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
        ])
    }];
};