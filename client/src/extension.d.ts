declare interface Window {
    ioc: any;
    jQuery: any;
    Sys: any;
}

interface StringConstructor {
    format(...params: Array<any>): string;
    isNullOrWhiteSpace(value: string): boolean;
    empty: string;
    firstCharToLower(str: string): string;
    toPascalCase(str: string): string;
}
interface Array<T> {
    firstOrDefault(callback: any): any;
    removeItem(item: any): Array<any>;
    any(callback: any): boolean;
    toString(saperator?: string): string;
    merge(items: Array<any>, predicate: any): Array<any>;
}

interface Route {
    name?: string;
}
declare namespace Stimulsoft.Viewer {
    export enum StiWebViewMode{
        WholeReport
    }
    export class StiViewerOptions {
        appearance: any;
        toolbar: any;
        width: any;
        height: any;
    }
    export class StiViewer {
        constructor(...params: Array<any>);

    }
}

declare namespace Stimulsoft.Report {
    export class StiReport {
        loadFile(path: string): void;
    }
}
declare namespace Stimulsoft.System.Drawing{
    export enum Color{
        navy
    }
}