import clr

clr.AddReferenceByName("PythonDemo")

from PythonDemo.Models import Contact
from PythonDemo.Scripting import IHookListener
import callbackManager

class hook_listener(IHookListener):
    """
    Python wrapper for IHookListener. Not really necessary in this case, but here for an example
    """
    def Callback(self, payload):
        pass

def registerCallback(event, listener):
    callbackManager.RegisterCallback(event, listener)