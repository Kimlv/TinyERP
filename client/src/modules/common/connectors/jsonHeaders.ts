import {Http, Headers} from "@angular/http";
export class JsonHeaders extends Headers {
    constructor() {
        super();
        this.append("Content-Type", "application/json");
        this.append("accept", "application/json");
    }
}