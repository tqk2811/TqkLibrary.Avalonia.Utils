export function RegisterModule(dotnetRuntime) {
    dotnetRuntime.setModuleImports(
        'clipboardHelper',
        {
            writeText: async (text) => {
                await globalThis.navigator.clipboard.writeText(text);
            },
            readText: async () => {
                return await globalThis.navigator.clipboard.readText();
            },
            checkPermissions: async () => {
                const readPermission = await globalThis.navigator.permissions.query({ name: 'clipboard-read' });
                const writePermission = await globalThis.navigator.permissions.query({ name: 'clipboard-write' });
                return JSON.stringify({
                    Read: readPermission?.state == 'granted',
                    Write: writePermission?.state == 'granted',
                });
            },
            requestReadPermissions: async () => {
                let readGranted = false;
                try {
                    await globalThis.navigator.clipboard.readText();
                    readGranted = true;
                } catch { }
                return readGranted;
            },
            requestWritePermissions: async () => {
                let writeGranted = false;
                try {
                    await globalThis.navigator.clipboard.writeText("permission-test");
                    writeGranted = true;
                } catch { }
                return writeGranted;
            }
        }
    )
    return dotnetRuntime;
}