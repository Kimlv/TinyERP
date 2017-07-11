let domHelper = {
    createScript: createScript,
    createStyle: createStyle
};
export default domHelper;
function createStyle(src: string, callback: any = null) {
    var style = document.createElement('link');
    style.onload = function () {
        if (callback) {
            callback();
        }
    };
    style.href = src;
    style.rel = "stylesheet";
    document.getElementsByTagName('head')[0].appendChild(style);
}
function createScript(src: string, callback: any = null) {
    var script = document.createElement('script');
    script.onload = function () {
        if (callback) {
            callback();
        }
    };
    script.src = src;
    script.async = false;
    document.getElementsByTagName('head')[0].appendChild(script);
}