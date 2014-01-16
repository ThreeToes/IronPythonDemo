#Import IronPython's clr module
import clr

#Add the assembly reference for our application
clr.AddReferenceByName("PythonDemo")

#Import our data structures in a normal python way
from PythonDemo.Models import Contact
from PythonDemo.Scripting import IHookListener

#Bit of a cheap thing to do, but this allows nicer looking python if we hide this behind our module
import callbackManager
#Needs to be this, this module is named the same thing
import contactManagerObject

class HookListener(IHookListener):
    """
    Python wrapper for IHookListener. Not really necessary in this case, but here for an example
    """
    def Callback(self, payload):
        pass

def registerCallback(event, listener):
    """
    Register a listener
    """
    callbackManager.RegisterCallback(event, listener)

def createContact():
    """
    Create a contact
    """
    return Contact()

def addContact(contact):
    """
    Add a contact to the application
    """
    contactManagerObject.Contacts.Add(contact)

def getContacts():
    """
    Get all contacts in the system
    """
    return contactManagerObject.Contacts