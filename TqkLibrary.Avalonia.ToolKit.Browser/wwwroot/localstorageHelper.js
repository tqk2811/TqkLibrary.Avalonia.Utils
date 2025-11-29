export function RegisterModule(dotnetRuntime){
    dotnetRuntime.setModuleImports(
        'localStorageHelper',
        {
            setItem: (key, value) => globalThis.localStorage.setItem(key, value),
            getItem: (key) => globalThis.localStorage.getItem(key),
        }
    )
    return dotnetRuntime;
}