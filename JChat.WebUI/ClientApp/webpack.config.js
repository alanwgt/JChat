const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

const devMode = process.env.NODE_ENV !== 'production';

const plugins = [
  new HtmlWebpackPlugin({
    title: 'JChat',
    minify: !devMode,
    hash: true,
    template: 'src/assets/index.html', // favicon: 'src/favicon.ico',
    inject: true,
  }),
];

const resolvePath = (...parts) => path.resolve(__dirname, ...parts);

const config = {
  entry: ['react-hot-loader/patch', './index.jsx'],
  output: {
    path: resolvePath('build'),
    filename: 'bundle.js',
    publicPath: '/',
  },
  resolve: {
    extensions: ['.js', '.jsx', '.ts', '.json'],
    alias: {
      '@': resolvePath('src'),
    },
  },
  mode: devMode ? 'development' : 'production',
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /(node_modules|bower_components)/,
        loader: 'babel-loader',
      },
      {
        test: /\.css$/,
        use: [
          devMode ? 'style-loader' : MiniCssExtractPlugin.loader,
          'css-loader',
        ],
      },
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
    ],
  },
};

if (devMode) {
  plugins.push(
    new webpack.SourceMapDevToolPlugin({
      filename: '[file].map',
    }),
    new webpack.HotModuleReplacementPlugin()
  );
  config.devtool = 'source-map';
  config.devServer = {
    static: {
      directory: path.resolve(__dirname, './public'),
      publicPath: '/',
    },
    port: 3333,
    hot: true,
    historyApiFallback: true,
    compress: true,
    // allowedHosts: ['jchat.alanwgt.com'],
    allowedHosts: 'all',
  };
} else {
  plugins.push(
    new MiniCssExtractPlugin({
      filename: '[name].[contenthash].css',
    })
  );
}

config.plugins = plugins;
module.exports = config;
