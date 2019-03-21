﻿/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v12.0.15.0 (NJsonSchema v9.13.22.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class OncologyPatientClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject('BASE_URL') baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "http://localhost:44330";
    }

    getAll(): Observable<OncologyPatientsListModel | null> {
        let url_ = this.baseUrl + "/api/OncologyPatient";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAll(<any>response_);
                } catch (e) {
                    return <Observable<OncologyPatientsListModel | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<OncologyPatientsListModel | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetAll(response: HttpResponseBase): Observable<OncologyPatientsListModel | null> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 ? OncologyPatientsListModel.fromJS(resultData200) : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<OncologyPatientsListModel | null>(<any>null);
    }

    createPatient(model: OncologyPatientModel): Observable<number> {
        let url_ = this.baseUrl + "/api/OncologyPatient";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json", 
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreatePatient(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreatePatient(<any>response_);
                } catch (e) {
                    return <Observable<number>><any>_observableThrow(e);
                }
            } else
                return <Observable<number>><any>_observableThrow(response_);
        }));
    }

    protected processCreatePatient(response: HttpResponseBase): Observable<number> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<number>(<any>null);
    }

    updatePatient(model: OncologyPatientModel): Observable<FileResponse | null> {
        let url_ = this.baseUrl + "/api/OncologyPatient";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json", 
                "Accept": "application/json"
            })
        };

        return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdatePatient(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdatePatient(<any>response_);
                } catch (e) {
                    return <Observable<FileResponse | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<FileResponse | null>><any>_observableThrow(response_);
        }));
    }

    protected processUpdatePatient(response: HttpResponseBase): Observable<FileResponse | null> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers.get("content-disposition") : undefined;
            const fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
            const fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            return _observableOf({ fileName: fileName, data: <any>responseBlob, status: status, headers: _headers });
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<FileResponse | null>(<any>null);
    }

    getPatient(id: number): Observable<OncologyPatientModel | null> {
        let url_ = this.baseUrl + "/api/OncologyPatient/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id)); 
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetPatient(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetPatient(<any>response_);
                } catch (e) {
                    return <Observable<OncologyPatientModel | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<OncologyPatientModel | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetPatient(response: HttpResponseBase): Observable<OncologyPatientModel | null> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 ? OncologyPatientModel.fromJS(resultData200) : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<OncologyPatientModel | null>(<any>null);
    }

    attemptCreatePatient(model: OncologyPatientModel): Observable<OncologyPatientsListModel | null> {
        let url_ = this.baseUrl + "/api/OncologyPatient/attempt";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(model);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json", 
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processAttemptCreatePatient(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processAttemptCreatePatient(<any>response_);
                } catch (e) {
                    return <Observable<OncologyPatientsListModel | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<OncologyPatientsListModel | null>><any>_observableThrow(response_);
        }));
    }

    protected processAttemptCreatePatient(response: HttpResponseBase): Observable<OncologyPatientsListModel | null> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 ? OncologyPatientsListModel.fromJS(resultData200) : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<OncologyPatientsListModel | null>(<any>null);
    }
}

export class OncologyPatientsListModel implements IOncologyPatientsListModel {
    items?: OncologyPatientModel[] | undefined;

    constructor(data?: IOncologyPatientsListModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            if (data["items"] && data["items"].constructor === Array) {
                this.items = [] as any;
                for (let item of data["items"])
                    this.items!.push(OncologyPatientModel.fromJS(item));
            }
        }
    }

    static fromJS(data: any): OncologyPatientsListModel {
        data = typeof data === 'object' ? data : {};
        let result = new OncologyPatientsListModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (this.items && this.items.constructor === Array) {
            data["items"] = [];
            for (let item of this.items)
                data["items"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IOncologyPatientsListModel {
    items?: OncologyPatientModel[] | undefined;
}

export class OncologyPatientModel implements IOncologyPatientModel {
    oncologyPatientId!: number;
    person?: PersonModel | undefined;
    registrationDate?: Date | undefined;
    admissionDate?: Date | undefined;
    informantsRelationship?: string | undefined;
    reasonForReferral?: string | undefined;

    constructor(data?: IOncologyPatientModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.oncologyPatientId = data["oncologyPatientId"];
            this.person = data["person"] ? PersonModel.fromJS(data["person"]) : <any>undefined;
            this.registrationDate = data["registrationDate"] ? new Date(data["registrationDate"].toString()) : <any>undefined;
            this.admissionDate = data["admissionDate"] ? new Date(data["admissionDate"].toString()) : <any>undefined;
            this.informantsRelationship = data["informantsRelationship"];
            this.reasonForReferral = data["reasonForReferral"];
        }
    }

    static fromJS(data: any): OncologyPatientModel {
        data = typeof data === 'object' ? data : {};
        let result = new OncologyPatientModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["oncologyPatientId"] = this.oncologyPatientId;
        data["person"] = this.person ? this.person.toJSON() : <any>undefined;
        data["registrationDate"] = this.registrationDate ? this.registrationDate.toISOString() : <any>undefined;
        data["admissionDate"] = this.admissionDate ? this.admissionDate.toISOString() : <any>undefined;
        data["informantsRelationship"] = this.informantsRelationship;
        data["reasonForReferral"] = this.reasonForReferral;
        return data; 
    }
}

export interface IOncologyPatientModel {
    oncologyPatientId: number;
    person?: PersonModel | undefined;
    registrationDate?: Date | undefined;
    admissionDate?: Date | undefined;
    informantsRelationship?: string | undefined;
    reasonForReferral?: string | undefined;
}

export class PersonModel implements IPersonModel {
    personId?: string | undefined;
    firstName?: string | undefined;
    middleName?: string | undefined;
    lastName?: string | undefined;
    additionalLastName?: string | undefined;
    preferredName?: string | undefined;
    governmentIDNumber?: string | undefined;
    address?: string | undefined;
    addressLine2?: string | undefined;
    city?: string | undefined;
    state?: string | undefined;
    country?: string | undefined;
    homePhone?: string | undefined;
    mobilePhone?: string | undefined;
    nationality?: string | undefined;
    race?: string | undefined;
    gender?: string | undefined;
    birthdate?: Date | undefined;
    birthplace?: string | undefined;
    familyStatus?: string | undefined;
    schoolLevel?: string | undefined;
    methodOfTranspotation?: string | undefined;

    constructor(data?: IPersonModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.personId = data["personId"];
            this.firstName = data["firstName"];
            this.middleName = data["middleName"];
            this.lastName = data["lastName"];
            this.additionalLastName = data["additionalLastName"];
            this.preferredName = data["preferredName"];
            this.governmentIDNumber = data["governmentIDNumber"];
            this.address = data["address"];
            this.addressLine2 = data["addressLine2"];
            this.city = data["city"];
            this.state = data["state"];
            this.country = data["country"];
            this.homePhone = data["homePhone"];
            this.mobilePhone = data["mobilePhone"];
            this.nationality = data["nationality"];
            this.race = data["race"];
            this.gender = data["gender"];
            this.birthdate = data["birthdate"] ? new Date(data["birthdate"].toString()) : <any>undefined;
            this.birthplace = data["birthplace"];
            this.familyStatus = data["familyStatus"];
            this.schoolLevel = data["schoolLevel"];
            this.methodOfTranspotation = data["methodOfTranspotation"];
        }
    }

    static fromJS(data: any): PersonModel {
        data = typeof data === 'object' ? data : {};
        let result = new PersonModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["personId"] = this.personId;
        data["firstName"] = this.firstName;
        data["middleName"] = this.middleName;
        data["lastName"] = this.lastName;
        data["additionalLastName"] = this.additionalLastName;
        data["preferredName"] = this.preferredName;
        data["governmentIDNumber"] = this.governmentIDNumber;
        data["address"] = this.address;
        data["addressLine2"] = this.addressLine2;
        data["city"] = this.city;
        data["state"] = this.state;
        data["country"] = this.country;
        data["homePhone"] = this.homePhone;
        data["mobilePhone"] = this.mobilePhone;
        data["nationality"] = this.nationality;
        data["race"] = this.race;
        data["gender"] = this.gender;
        data["birthdate"] = this.birthdate ? this.birthdate.toISOString() : <any>undefined;
        data["birthplace"] = this.birthplace;
        data["familyStatus"] = this.familyStatus;
        data["schoolLevel"] = this.schoolLevel;
        data["methodOfTranspotation"] = this.methodOfTranspotation;
        return data; 
    }
}

export interface IPersonModel {
    personId?: string | undefined;
    firstName?: string | undefined;
    middleName?: string | undefined;
    lastName?: string | undefined;
    additionalLastName?: string | undefined;
    preferredName?: string | undefined;
    governmentIDNumber?: string | undefined;
    address?: string | undefined;
    addressLine2?: string | undefined;
    city?: string | undefined;
    state?: string | undefined;
    country?: string | undefined;
    homePhone?: string | undefined;
    mobilePhone?: string | undefined;
    nationality?: string | undefined;
    race?: string | undefined;
    gender?: string | undefined;
    birthdate?: Date | undefined;
    birthplace?: string | undefined;
    familyStatus?: string | undefined;
    schoolLevel?: string | undefined;
    methodOfTranspotation?: string | undefined;
}

export interface FileResponse {
    data: Blob;
    status: number;
    fileName?: string;
    headers?: { [name: string]: any };
}

export class SwaggerException extends Error {
    message: string;
    status: number; 
    response: string; 
    headers: { [key: string]: any; };
    result: any; 

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if(result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader(); 
            reader.onload = event => { 
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob); 
        }
    });
}
