System.config({
    defaultJSExtensions: true,
    baseUrl: '.',
    paths: {
        npm: "node_modules/",
        "@app/common": ["src/modules/common/index.js"],
        "@app/security": ["src/modules/security/index.js"],
        "@app/setting": ["src/modules/setting/index.js"],
        "@app/themes/default": ["src/themes/default/index.js"],
        "@app/customerManagement": ["src/modules/customerManagement/index.js"]
    },
    map: {
        "@app/common": "src/modules/common/index.js",
        "@angular/core": 'npm@angular/core/bundles/core.umd.js',
        '@angular/common': 'npm@angular/common/bundles/common.umd.js',
        '@angular/compiler': 'npm@angular/compiler/bundles/compiler.umd.js',
        '@angular/platform-browser': 'npm@angular/platform-browser/bundles/platform-browser.umd.js',
        '@angular/platform-browser-dynamic': 'npm@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
        '@angular/http': 'npm@angular/http/bundles/http.umd.js',
        '@angular/router': 'npm@angular/router/bundles/router.umd.js',
        '@angular/forms': 'npm@angular/forms/bundles/forms.umd.js',
        'rxjs': 'npmrxjs',
        'ts': 'npm@plugin-typescript@4.0.10/lib/plugin.js',
        'typescript': 'npm@typescript@2.0.3/lib/typescript.js',
        'ej-angular2': 'npmej-angular2',
        'syncfusion-javascript': 'npmsyncfusion-javascript',
        'jsrender': 'npmjsrender',
        'jquery': 'npmjquery',
        "jquery-validation":"npmjquery-validation"
    },
    packages: {
        "jquery-validation":{
            main:"dist/jquery.validate.js"
        },
        "jquery":{
            main:"dist/jquery.js"
        },
        "jsrender":{
            main:"jsrender.js"
        },
        "@app/common": {
            defaultExtension: "js"
        },
        api: {
            defaultExtension: "json"
        },
        src: {
            main: "./main.ts",
            defaultExtension: "js"
        },
        rxjs: {
            defaultExtension: "js"
        },
        'ej-angular2': {
            main: './src/index.js'
        }
    }
});
