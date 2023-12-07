import {Injectable} from "@angular/core";
import {User} from "./models";
import {Avatar} from "./app/products/products.service";

@Injectable({
  providedIn: 'root'
})
export class State {
  users: User[] = [];
  avatar: Avatar[] = [];
}
