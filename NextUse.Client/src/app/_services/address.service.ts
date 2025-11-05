import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Address } from '../_models/address';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private readonly apiUrl = environment.apiUrl + 'Address';
  constructor(private http: HttpClient) { }

  create(address: Address): Observable<Address> {
    return this.http.post<Address>(this.apiUrl, address);
  }

  update(addressId:number, address: Address): Observable<Address> {
    return this.http.put<Address>(this.apiUrl + '/' + addressId, address);
  }

  findById(addressId: number): Observable<Address> {
    return this.http.get<Address>(this.apiUrl + '/' + addressId);
  }

  //FUTURE WORK
  // GET ALL Addresses, Address API
  
}

