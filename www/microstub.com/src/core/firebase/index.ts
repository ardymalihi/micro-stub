import {
  AuthMethods,
  defaultFirebase,
  FIREBASE_PROVIDERS,
  firebaseAuthConfig
} from 'angularfire2';


export const FIREBASE_APP_PROVIDERS: any[] = [
  FIREBASE_PROVIDERS,
  defaultFirebase({
    apiKey: "AIzaSyCp1llqPPIoei2JuWcJInfcgZK2TeeWfQ0",
    authDomain: "microstub-35829.firebaseapp.com",
    databaseURL: "https://microstub-35829.firebaseio.com",
    storageBucket: "microstub-35829.appspot.com",
  }),
  firebaseAuthConfig({
    method: AuthMethods.Popup,
    remember: 'default'
  })
];
