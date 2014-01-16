import clr

clr.AddReferenceByName("PythonDemo")

from PythonDemo.Models import Contact
from PythonDemo.Scripting import IHookListener

class hook_listener(IHookListener):
    """
    Python wrapper for IHookListener
    """
    def Callback(self, payload):
        pass

def registerCallback(event, listener):
    """
    Register a callback with the application
    """
    callbackManager.RegisterCallback(event, listener)
