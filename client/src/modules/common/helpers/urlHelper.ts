const DEFAULT_PORT = 80;
const HTTP_PROTOCOL_PREFIX = "http:";
const HTTPS_PROTOCOL_PREFIX = "https:";
let urlHelper = {
    resolve: resolve
};
export default urlHelper;
function resolve(url: string) {
    if (isAbsoluteUrl(url)) { return url; }
    let resolvedUrl = String.format(
        "{0}//{1}/{2}",
        window.location.protocol,
        window.location.host,
        url
    );
    return resolvedUrl;
}
function isAbsoluteUrl(url: string): boolean {
    return !String.isNullOrWhiteSpace(url) && (url.startsWith(HTTP_PROTOCOL_PREFIX) || url.startsWith(HTTPS_PROTOCOL_PREFIX));
}